using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Dtos;
using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.Services;

internal class ShiftService
{
    internal static async Task InsertShift()
    {
        var shift = new CreateShiftRequestDto();
        // TODO: These date times should be some kind of an input
        shift.StartTime = DateTime.Now;
        shift.EndTime = DateTime.Now;

        var employee = await EmployeeService.GetEmployeeOptionInput();
        shift.EmployeeId = employee.Id;
        await ShiftController.AddShift(shift);
    }
    internal static void UpdateShift()
    {
        throw new NotImplementedException();
    }
    internal static async Task<List<Shift>> GetAllShiftsByEmployee()
    {
        var employee = await EmployeeService.GetEmployeeOptionInput();

        // get list of shifts
        var shifts = await ShiftController.GetAllShifts();

        var shiftsForEmployee = shifts
            .Where(s => s.EmployeeId == employee.Id)
            .Select(s => new Shift
            {
                Id = s.Id,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
            })
            .ToList();

        return shiftsForEmployee;
    }

    internal static async Task DeleteShift()
    {
        var shift = await GetShiftOptionInputId();
        await ShiftController.DeleteShift(shift);
    }

    public static async Task ShowShiftForEmployee()
    {
        var shifts = await GetAllShiftsByEmployee();
        UserInterface.ShowShiftsTable(shifts);
    }

    public static async Task<int> GetShiftOptionInputId()
    {
        var shifts = await GetAllShiftsByEmployee();
        //Console.WriteLine($"Employee: {employee.Name}");
        foreach (var shift in shifts)
        {
            Console.WriteLine("Please choose from the following shifts");
            Console.WriteLine($"- Shift ID: {shift.Id}, Start Time: {shift.StartTime}, End Time: {shift.EndTime}");
            var shiftDurationMinutes = shift.StartTime.Minute + shift.EndTime.Minute;
            TimeSpan totalTime = shift.GetTotalTime();
            Console.WriteLine($"Total time: {totalTime}");
        }
        Console.WriteLine();
        var shiftsArray = shifts.Select(x => x.Id.ToString())
            .ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a shift")
            .AddChoices(shiftsArray));
        var id = shifts.Single(x => x.Id.ToString() == option).Id;

        return id;
    }
}

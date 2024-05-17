using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Dtos;
using ShiftsLoggerUI.Models;
using Spectre.Console;
using System.Globalization;

namespace ShiftsLoggerUI.Services;

internal class ShiftService
{
    internal static async Task InsertShift()
    {
        var shift = new CreateShiftRequestDto();
        var employee = await EmployeeService.GetEmployeeOptionInput();
        shift.EmployeeId = employee.Id;
        shift.StartTime = Helpers.GetDateAndTime("Start Time");
        shift.EndTime = Helpers.GetDateAndTime("End Time");
        bool validTimes = false;

        while (!validTimes)
        {

            if (shift.StartTime >= shift.EndTime)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red]Error: Start time must be before end time.[/]");
                shift.StartTime = Helpers.GetDateAndTime("Start Time");
                shift.EndTime = Helpers.GetDateAndTime("End Time");
            }
            else
            {
                validTimes = true;
            }
        }

        await ShiftController.AddShift(shift);
    }

    internal static async Task UpdateShift()
    {
        var shift = new UpdateShiftRequestDto();
        var shiftId = await GetShiftOptionInputId();
        shift.Id = shiftId;
        shift.StartTime = Helpers.GetDateAndTime("Start Time");
        shift.EndTime = Helpers.GetDateAndTime("End Time");
        bool validTimes = false;

        while (!validTimes)
        {

            if (shift.StartTime >= shift.EndTime)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red]Error: Start time must be before end time.[/]");
                shift.StartTime = Helpers.GetDateAndTime("Start Time");
                shift.EndTime = Helpers.GetDateAndTime("End Time");
            }
            else
            {
                validTimes = true; // Set to true when valid times are entered
            }
        }

        await ShiftController.UpdateShift(shift);
    }
    internal static async Task<List<Shift>> GetAllShiftsByEmployee()
    {
        var employee = await EmployeeService.GetEmployeeOptionInput();
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

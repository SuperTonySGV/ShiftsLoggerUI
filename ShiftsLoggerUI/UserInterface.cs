﻿using Newtonsoft.Json;
using RestSharp;
using ShiftsLoggerUI.Models;
using ShiftsLoggerUI.Services;
using Spectre.Console;

namespace ShiftsLoggerUI;

static internal class UserInterface
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MainMenuOptions.ManageEmployees,
                    MainMenuOptions.ManageShifts,
                    MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.ManageEmployees:
                    EmployeesMenu();
                    break;
                case MainMenuOptions.ManageShifts:
                    ShiftsMenu();
                    break;
                case MainMenuOptions.Quit:
                    isAppRunning = false;
                    break;
            }
        }
    }

    static internal void EmployeesMenu()
    {
        var isEmployeesMenuRunning = true;
        while (isEmployeesMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<EmployeeMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    EmployeeMenu.AddEmployee,
                    EmployeeMenu.DeleteEmployee,
                    EmployeeMenu.UpdateEmployee,
                    EmployeeMenu.ViewEmployee,
                    EmployeeMenu.ViewAllEmployees,
                    EmployeeMenu.GoBack));

            switch (option)
            {
                case EmployeeMenu.AddEmployee:
                    EmployeeService.InsertEmployee().Wait();
                    break;
                case EmployeeMenu.DeleteEmployee:
                    EmployeeService.DeleteEmployee().Wait();
                    break;
                case EmployeeMenu.UpdateEmployee:
                    EmployeeService.UpdateEmployee().Wait();
                    break;
                case EmployeeMenu.ViewEmployee:
                    EmployeeService.GetEmployee().Wait();
                    break;
                case EmployeeMenu.ViewAllEmployees:
                    EmployeeService.GetEmployees().Wait();
                    break;
                case EmployeeMenu.GoBack:
                    MainMenu();
                    break;
            }
        }
    }

    static internal void ShiftsMenu()
    {
        var isShiftsMenuRunning = true;
        while (isShiftsMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ShiftMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    ShiftMenu.AddShift,
                    ShiftMenu.DeleteShift,
                    ShiftMenu.UpdateShift,
                    ShiftMenu.ViewAllShifts,
                    ShiftMenu.GoBack));

            switch (option)
            {
                case ShiftMenu.AddShift:
                    ShiftService.InsertShift().Wait();
                    break;
                case ShiftMenu.DeleteShift:
                    ShiftService.DeleteShift().Wait();
                    break;
                case ShiftMenu.UpdateShift:
                    ShiftService.UpdateShift();
                    break;
                case ShiftMenu.ViewAllShifts:
                    ShiftService.ShowShiftForEmployee().Wait();
                    break;
                case ShiftMenu.GoBack:
                    MainMenu();
                    break;
            }
        }
    }

    internal static void ShowEmployee(Employee employee)
    {
        var panel = new Panel($"Id: {employee.Id}\nName: {employee.Name}");
        panel.Header = new PanelHeader("Employee Information");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowEmployeeTable(List<Employee> employees)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var employee in employees)
        {
            table.AddRow(employee.Id.ToString(), employee.Name);
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowShiftsTable(List<Shift> shifts)
    {
        var table = new Table();
        table.AddColumn("Shift ID");
        table.AddColumn("Start time");
        table.AddColumn("End time");

        foreach (var shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.StartTime.ToString(), shift.EndTime.ToString());
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    internal enum MainMenuOptions
    {
        ManageEmployees,
        ManageShifts,
        Quit
    }

    internal enum EmployeeMenu
    {
        AddEmployee,
        UpdateEmployee,
        DeleteEmployee,
        ViewEmployee,
        ViewAllEmployees,
        GoBack
    }

    internal enum ShiftMenu
    {
        AddShift,
        UpdateShift,
        DeleteShift,
        ViewShift,
        ViewAllShifts,
        GoBack,
    }
}

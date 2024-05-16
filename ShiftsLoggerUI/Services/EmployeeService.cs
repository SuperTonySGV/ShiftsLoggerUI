using Newtonsoft.Json;
using RestSharp;
using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Models;
using Spectre.Console;
using ShiftsLoggerUI.Services;
using ShiftsLoggerUI.Dtos;

namespace ShiftsLoggerUI.Services;

public class EmployeeService
{
    internal static async Task InsertEmployee()
    {
        var employee = new CreateEmployeeRequestDto();
        employee.Name = AnsiConsole.Ask<string>("What is the employee name?");
        await EmployeeController.AddEmployee(employee);
    }

    internal static async Task UpdateEmployee()
    {
        var employee = await GetEmployeeOptionInput();
        var updatedEmployee = new UpdateEmployeeRequestDto();
        updatedEmployee.Name = AnsiConsole.Confirm("Update name?") ? AnsiConsole.Ask<string>("What is the new name of your employee?") : employee.Name;
        updatedEmployee.Id = employee.Id;

        await EmployeeController.UpdateEmployee(updatedEmployee);
    }

    public static async Task GetEmployee()
    {
        var employee = await GetEmployeeOptionInput();
        UserInterface.ShowEmployee(employee);
    }

    public static async Task GetEmployees()
    {
        var employees = await EmployeeController.GetAllEmployees();
        UserInterface.ShowEmployeeTable(employees);
    }

    internal static async Task DeleteEmployee()
    {
        var employee = await GetEmployeeOptionInput();
        await EmployeeController.DeleteEmployee(employee);
    }

    public static async Task<Employee> GetEmployeeOptionInput()
    {
        var employees = await EmployeeController.GetAllEmployees();
        var employeesArray = employees.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a employee")
            .AddChoices(employeesArray));
        var id = employees.Single(x => x.Name == option).Id;
        var employee = await EmployeeController.GetEmployeeById(id);

        return employee;
    }
}
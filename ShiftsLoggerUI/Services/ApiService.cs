using Newtonsoft.Json;
using ShiftsLoggerUI.Models;
using Spectre.Console;
using System.Net.Http;
using System.Net.Http.Json;

namespace ShiftsLoggerUI.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7131/api/");
    }

    public async Task<Employee> GetAllEmployeesAsync()
    {
        var response = await _httpClient.GetAsync($"Employee");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var employee = JsonConvert.DeserializeObject<Employee>(jsonString);

        return employee;
    }

    public async Task<Employee> GetEmployeeAsync(int employeeId)
    {
        var response = await _httpClient.GetAsync($"Employee/{employeeId}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var employee = JsonConvert.DeserializeObject<Employee>(jsonString);

        return employee;
    }
}

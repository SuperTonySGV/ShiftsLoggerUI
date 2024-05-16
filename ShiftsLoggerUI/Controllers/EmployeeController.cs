using Newtonsoft.Json;
using RestSharp;
using ShiftsLoggerUI.Dtos;
using ShiftsLoggerUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace ShiftsLoggerUI.Controllers;

internal class EmployeeController
{

    public static async Task AddEmployee(CreateEmployeeRequestDto employeeDto)
    {
        Console.Clear();

        string apiEndPoint = "https://localhost:7131/api/Employee";

        try
        {
            using (HttpClient client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(employeeDto);
                var postData = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiEndPoint, postData);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("POST request successful!");
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); Console.ReadLine();
        }
    }

    public static async Task UpdateEmployee(UpdateEmployeeRequestDto employeeDto)
    {
        Console.Clear();

        string apiEndPoint = $"https://localhost:7131/api/Employee/{employeeDto.Id}";

        try
        {
            using (HttpClient client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(employeeDto);
                var postData = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(apiEndPoint, postData);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("POST request successful!");
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); Console.ReadLine();
        }
    }

    public static async Task<List<Employee>> GetAllEmployees()
    {
        //Console.Clear();

        string apiEndPoint = "https://localhost:7131/api/Employee";

        List<Employee> employees = new List<Employee>();
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiEndPoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    employees = JsonConvert.DeserializeObject<List<Employee>>(data);

                    return employees;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); Console.ReadLine();
        }

        return null;
    }

    public static async Task<Employee> GetEmployeeById(int id)
    {
        Console.Clear();

        string apiEndPoint = $"https://localhost:7131/api/Employee/{id}";

        Employee employee = new Employee();
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiEndPoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    employee = JsonConvert.DeserializeObject<Employee>(data);

                    return employee;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); Console.ReadLine();
        }

        return null;
    }

    public static async Task<Employee> DeleteEmployee(Employee employee)
    {
        Console.Clear();

        string apiEndPoint = $"https://localhost:7131/api/Employee/{employee.Id}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(apiEndPoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    employee = JsonConvert.DeserializeObject<Employee>(data);

                    return employee;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); Console.ReadLine();
        }

        return null;
    }
}

using Newtonsoft.Json;
using RestSharp;
using ShiftsLoggerUI.Dtos;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI.Controllers;

internal class ShiftController
{
    public static async Task AddShift(CreateShiftRequestDto shiftDto)
    {
        Console.Clear();

        string apiEndPoint = "https://localhost:7131/api/Shift";

        try
        {
            using (HttpClient client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(shiftDto);
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

    public static async Task<List<Shift>> GetAllShifts()
    {
        Console.Clear();

        string apiEndPoint = "https://localhost:7131/api/Shift";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                List<Shift> shifts = new List<Shift>();
                HttpResponseMessage response = await client.GetAsync(apiEndPoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    shifts = JsonConvert.DeserializeObject<List<Shift>>(data);

                    return shifts;
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

    public static async Task DeleteShift(int id)
    {
        Console.Clear();

        string apiEndPoint = $"https://localhost:7131/api/Shift/{id}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(apiEndPoint);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Shift deleted: {data}");
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
}

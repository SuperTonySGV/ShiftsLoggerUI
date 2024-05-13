using Newtonsoft.Json;
using RestSharp;
using ShiftsLoggerUI.Models;

Console.WriteLine("Press any key to do the API request");
Console.ReadLine();

var client = new RestClient("https://localhost:7131/api/");
var request = new RestRequest("Employee");
var response = client.ExecuteAsync(request);

List<Employee> employees = new();

if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
{
    string rawReponse = response.Result.Content;
    var serialize = JsonConvert.DeserializeObject<Employees>(rawReponse);
    // Cannot deserialize the current JSON array ^^^

    employees = serialize.EmployeesList;
    TableVisualizationEngine.ShowTable(employees, "Employees");
}

Console.WriteLine("All done.");
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Shift> Shifts { get; set; }
}

public class Employees
{
    public List<Employee> EmployeesList { get; set; }
}

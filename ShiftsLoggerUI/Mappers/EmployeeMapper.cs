using ShiftsLoggerUI.Dtos;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI.Mappers;

public static class EmployeeMapper
{
    public static Employee ToEmployeeFromCreateDto(this CreateEmployeeRequestDto employeeDto)
    {
        return new Employee
        {
            Name = employeeDto.Name
        };
    }

    public static Employee ToEmployeeFromUpdateDto(this UpdateEmployeeRequestDto employeeDto)
    {
        return new Employee
        {
            Id = employeeDto.Id,
            Name = employeeDto.Name
        };
    }
}

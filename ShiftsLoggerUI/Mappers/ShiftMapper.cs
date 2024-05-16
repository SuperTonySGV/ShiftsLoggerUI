using ShiftsLoggerUI.Dtos;
using ShiftsLoggerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI.Mappers;

public static class ShiftMapper
{
    public static Shift ToShiftFromCreateDto(this CreateShiftRequestDto shiftDto)
    {
        return new Shift
        {
            StartTime = shiftDto.StartTime,
            EndTime = shiftDto.EndTime,
            EmployeeId = shiftDto.EmployeeId
        };
    }
}


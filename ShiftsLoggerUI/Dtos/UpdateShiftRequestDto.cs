using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI.Dtos;

public class UpdateShiftRequestDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }

    public TimeSpan GetTotalTime()
    {
        return EndTime - StartTime;
    }
}

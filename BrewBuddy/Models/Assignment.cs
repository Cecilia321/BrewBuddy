using System;
using System.Collections.Generic;

namespace BrewBuddy.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }
    public string Description { get; set; }
    public DateTime? DailyDate { get; set; }
    public string IntervalType { get; set; }
    public int MachineId { get; set; }

    public string AssignmentName { get; set; } = null!;

    public bool IsComplete { get; set; }

    public DateTime? FinishedDateAndTime { get; set; }
    

    public virtual ICollection<MachineInfo> MachineInfos { get; set; } = new List<MachineInfo>();
    public virtual CoffieMachine Machine { get; set; } = null!;
}

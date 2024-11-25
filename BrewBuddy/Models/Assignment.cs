using System;
using System.Collections.Generic;

namespace BrewBuddy.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public string AssignmentName { get; set; } = null!;

    public bool IsComplete { get; set; }

    public DateTime? DateAndTime { get; set; }

    public virtual ICollection<MachineInfo> MachineInfos { get; set; } = new List<MachineInfo>();
}

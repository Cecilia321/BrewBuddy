using System;
using System.Collections.Generic;

namespace BrewBuddy.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public bool IsComplete { get; set; }

    public DateTime? DateAndTime { get; set; }

    public virtual ICollection<MachineInfo> MachineInfos { get; set; } = new List<MachineInfo>();
}

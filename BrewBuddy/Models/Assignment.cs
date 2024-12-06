using System;
using System.Collections.Generic;

namespace BrewBuddy.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public string? Description { get; set; }

    public DateTime? DailyDate { get; set; }

    public string? IntervalType { get; set; }

    public int MachineId { get; set; }

    public int? UserId { get; set; }

    public string Type { get; set; } = null!;

    public decimal? Amount { get; set; }

    public bool IsComplete { get; set; }

    public DateTime? FinishedDateAndTime { get; set; }

    public virtual CoffieMachine? Machine { get; set; }

    public virtual User? User { get; set; }
}

using System;
using System.Collections.Generic;

namespace BrewBuddy.Models;

public partial class Statistik
{
    public int StatId { get; set; }

    public int? MachineId { get; set; }

    public DateOnly? FinishDateAndTime { get; set; }

    public string? Type { get; set; }

    public decimal? Amount { get; set; }

    public decimal? MonthlyAmount { get; set; }
}

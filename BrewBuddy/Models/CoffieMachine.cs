using System;
using System.Collections.Generic;

namespace BrewBuddy.Models;

public partial class CoffieMachine
{
    public int MachineId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateOnly? RentDate { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}

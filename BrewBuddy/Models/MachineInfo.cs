﻿using System;
using System.Collections.Generic;

namespace BrewBuddy.Models;

public partial class MachineInfo
{
    public int InfoId { get; set; }

    public int MachineId { get; set; }

    public int UserId { get; set; }

    public int AssignmentId { get; set; }

    public virtual CoffieMachine Machine { get; set; } = null!;

    public virtual Assignment Assignment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

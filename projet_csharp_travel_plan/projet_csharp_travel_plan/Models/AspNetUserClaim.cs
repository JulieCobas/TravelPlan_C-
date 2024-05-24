﻿using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Aspnetuserclaim
{
    public int Id { get; set; }

    public string Userid { get; set; } = null!;

    public string? Claimtype { get; set; }

    public string? Claimvalue { get; set; }

    public virtual Aspnetuser User { get; set; } = null!;
}

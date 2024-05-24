using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Aspnetusertoken
{
    public string Userid { get; set; } = null!;

    public string Loginprovider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual Aspnetuser User { get; set; } = null!;
}

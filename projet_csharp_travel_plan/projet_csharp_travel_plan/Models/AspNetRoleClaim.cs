using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Aspnetroleclaim
{
    public int Id { get; set; }

    public string Roleid { get; set; } = null!;

    public string? Claimtype { get; set; }

    public string? Claimvalue { get; set; }

    public virtual Aspnetrole Role { get; set; } = null!;
}

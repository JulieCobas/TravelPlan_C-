using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Aspnetuserlogin
{
    public string Loginprovider { get; set; } = null!;

    public string Providerkey { get; set; } = null!;

    public string? Providerdisplayname { get; set; }

    public string Userid { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
}

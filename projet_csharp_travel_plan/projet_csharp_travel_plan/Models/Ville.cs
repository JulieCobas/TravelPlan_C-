using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Ville
{
    public short IdVille { get; set; }

    public short IdRegion { get; set; }

    public string Nom { get; set; } = null!;

    public virtual Region IdRegionNavigation { get; set; } = null!;
}

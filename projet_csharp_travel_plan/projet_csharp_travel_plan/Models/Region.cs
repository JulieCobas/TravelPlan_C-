using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Region
{
    public short IdRegion { get; set; }

    public short IdPays { get; set; }

    public string Nom { get; set; } = null!;

    public virtual Pay IdPaysNavigation { get; set; } = null!;

    public virtual ICollection<Ville> Villes { get; set; } = new List<Ville>();
}

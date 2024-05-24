using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class NumChambre
{
    public short IdNumChambre { get; set; }

    public int NumeroChambre { get; set; }

    public virtual ICollection<DisponibiliteLogement> DisponibiliteLogements { get; set; } = new List<DisponibiliteLogement>();
}

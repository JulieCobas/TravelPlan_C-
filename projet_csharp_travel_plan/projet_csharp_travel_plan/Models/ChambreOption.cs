using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class ChambreOption
{
    public int IdChambreOption { get; set; }

    public bool PetitDejeunerInclus { get; set; }

    public bool AnnulationGratuite { get; set; }

    public DateOnly? DateAnnulationGratuite { get; set; }

    public virtual ICollection<Chambre> Chambres { get; set; } = new List<Chambre>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class ChambreOption
{
    public short IdChambreOption { get; set; }

    public bool PetitDejeunerInclus { get; set; }

    public bool AnnulationGratuite { get; set; }

    public DateTime? DateAnnulationGratuite { get; set; }

    public virtual ICollection<Chambre> Chambres { get; set; } = new List<Chambre>();
}

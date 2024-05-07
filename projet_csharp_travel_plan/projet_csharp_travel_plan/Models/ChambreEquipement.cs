using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class ChambreEquipement
{
    public int IdEquipChambre { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Chambre> IdChambres { get; set; } = new List<Chambre>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class EquipementCategorie
{
    public short IdCatEquipement { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Logement> IdLogements { get; set; } = new List<Logement>();
}

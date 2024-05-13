using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class EquipementCategorie
{
    [Key]
    public int IdCatEquipement { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Logement> IdLogements { get; set; } = new List<Logement>();
}

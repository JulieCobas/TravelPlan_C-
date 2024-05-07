using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class LogementCategorie
{
    public int IdLogementCategorie { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Logement> Logements { get; set; } = new List<Logement>();
}

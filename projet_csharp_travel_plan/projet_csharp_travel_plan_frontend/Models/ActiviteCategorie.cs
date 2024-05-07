using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class ActiviteCategorie
{
    public int IdCatActiv { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();
}

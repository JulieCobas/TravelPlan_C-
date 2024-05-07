using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class ActivitePrix
{
    public int IdPrixActivite { get; set; }

    public DateOnly DateDebutValidite { get; set; }

    public DateOnly DateFinValidite { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();

    public virtual ICollection<CategoriePrix> IdCategoriePrixes { get; set; } = new List<CategoriePrix>();
}

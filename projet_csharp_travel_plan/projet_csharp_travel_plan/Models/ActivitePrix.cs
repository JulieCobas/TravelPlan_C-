using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class ActivitePrix
{
    public short IdPrixActivite { get; set; }

    public DateTime DateDebutValidite { get; set; }

    public DateTime DateFinValidite { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();

    public virtual ICollection<CategoriePrix> IdCategoriePrixes { get; set; } = new List<CategoriePrix>();
}

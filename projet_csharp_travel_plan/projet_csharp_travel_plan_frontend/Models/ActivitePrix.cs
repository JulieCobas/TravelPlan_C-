using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class ActivitePrix
{
    [Key]
    public int IdPrixActivite { get; set; }

    public DateOnly DateDebutValidite { get; set; }

    public DateOnly DateFinValidite { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();

    public virtual ICollection<CategoriePrix> IdCategoriePrixes { get; set; } = new List<CategoriePrix>();
}

using projet_csharp_travel_plan_frontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class CategoriePrix
{
    [Key]
    public int IdCategoriePrix { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<PrixLogement> IdLogementPrixes { get; set; } = new List<PrixLogement>();

    public virtual ICollection<ActivitePrix> IdPrixActivites { get; set; } = new List<ActivitePrix>();

    public virtual ICollection<TransportPrix> IdPrixTransports { get; set; } = new List<TransportPrix>();
}

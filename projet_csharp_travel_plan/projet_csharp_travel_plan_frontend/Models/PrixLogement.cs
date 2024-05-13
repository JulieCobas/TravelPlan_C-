using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class PrixLogement
{
    [Key]
    public int IdLogementPrix { get; set; }

    public DateOnly DateDebutValidite { get; set; }

    public DateOnly DateFinValidite { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<Chambre> Chambres { get; set; } = new List<Chambre>();

    public virtual ICollection<LocationLogement> LocationLogements { get; set; } = new List<LocationLogement>();

    public virtual ICollection<CategoriePrix> IdCategoriePrixes { get; set; } = new List<CategoriePrix>();
}

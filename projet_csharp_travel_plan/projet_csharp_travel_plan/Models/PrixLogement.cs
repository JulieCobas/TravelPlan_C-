using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class PrixLogement
{
    public short IdLogementPrix { get; set; }

    public DateTime DateDebutValidite { get; set; }

    public DateTime DateFinValidite { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<Chambre> Chambres { get; set; } = new List<Chambre>();

    public virtual ICollection<Logement> Logements { get; set; } = new List<Logement>();

    public virtual ICollection<CategoriePrix> IdCategoriePrixes { get; set; } = new List<CategoriePrix>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Client
{
    public short IdClient { get; set; }

    public string? Id { get; set; }

    public string Addresse { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Ville { get; set; } = null!;

    public string Pays { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public DateTime DateNaissance { get; set; }

    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();

    public virtual ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
}

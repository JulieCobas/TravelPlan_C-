using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Fournisseur
{
    public int IdFournisseur { get; set; }

    public string NomCompagnie { get; set; } = null!;

    public string Adresse { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Ville { get; set; } = null!;

    public string Pays { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string? Telephone { get; set; }

    public string CompteBancaire { get; set; } = null!;

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();

    public virtual ICollection<Logement> Logements { get; set; } = new List<Logement>();

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}

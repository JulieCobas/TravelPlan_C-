using projet_csharp_travel_plan_frontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class Pay
{
    [Key]
    public int IdPays { get; set; }

    public string Nom { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Ville { get; set; } = null!;

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();

    public virtual ICollection<Logement> Logements { get; set; } = new List<Logement>();

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();

    public virtual ICollection<Voyage> IdVoyages { get; set; } = new List<Voyage>();
}

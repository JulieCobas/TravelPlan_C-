using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class LocationLogement
{
    [Key]
    public int IdLogementLoc { get; set; }

    public int IdLogementPrix { get; set; }

    public string Nom { get; set; } = null!;

    public virtual PrixLogement IdLogementPrixNavigation { get; set; } = null!;

    public virtual ICollection<Logement> Logements { get; set; } = new List<Logement>();
}

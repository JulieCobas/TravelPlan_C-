using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class LogementCategorie
{
    [Key]
    public int IdLogementCategorie { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Logement> Logements { get; set; } = new List<Logement>();
}

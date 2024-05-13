using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class ChambreEquipement
{
    [Key]
    public int IdEquipChambre { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Chambre> IdChambres { get; set; } = new List<Chambre>();
}

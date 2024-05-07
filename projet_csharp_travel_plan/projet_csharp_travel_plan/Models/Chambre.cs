using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Chambre
{
    public int IdChambre { get; set; }

    public int IdLogementPrix { get; set; }

    public int IdLogement { get; set; }

    public int? IdChambreOption { get; set; }

    public string Nom { get; set; } = null!;

    public string TypeDeChambre { get; set; } = null!;

    public short Surface { get; set; }

    public short NbOccupants { get; set; }

    public string? DetailsChambre { get; set; }

    public virtual ChambreOption? IdChambreOptionNavigation { get; set; }

    public virtual Logement IdLogementNavigation { get; set; } = null!;

    public virtual PrixLogement IdLogementPrixNavigation { get; set; } = null!;

    public virtual ICollection<ChambreEquipement> IdEquipChambres { get; set; } = new List<ChambreEquipement>();
}

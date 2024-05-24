using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Chambre
{
    public short IdChambre { get; set; }

    public short IdLogementPrix { get; set; }

    public short IdLogement { get; set; }

    public short? IdChambreOption { get; set; }

    public string Nom { get; set; } = null!;

    public string? TypeDeChambre { get; set; }

    public byte Surface { get; set; }

    public byte NbOccupants { get; set; }

    public string? DetailsChambre { get; set; }

    public virtual ICollection<DisponibiliteLogement> DisponibiliteLogements { get; set; } = new List<DisponibiliteLogement>();

    public virtual ChambreOption? IdChambreOptionNavigation { get; set; }

    public virtual Logement IdLogementNavigation { get; set; } = null!;

    public virtual PrixLogement IdLogementPrixNavigation { get; set; } = null!;

    public virtual ICollection<ChambreEquipement> IdEquipChambres { get; set; } = new List<ChambreEquipement>();
}

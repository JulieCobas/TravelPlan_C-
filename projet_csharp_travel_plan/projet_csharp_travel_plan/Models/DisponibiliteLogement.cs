using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class DisponibiliteLogement
{
    public short IdChambre { get; set; }

    public short IdNumChambre { get; set; }

    public bool Disponible { get; set; }

    public virtual Chambre IdChambreNavigation { get; set; } = null!;

    public virtual NumChambre IdNumChambreNavigation { get; set; } = null!;
}

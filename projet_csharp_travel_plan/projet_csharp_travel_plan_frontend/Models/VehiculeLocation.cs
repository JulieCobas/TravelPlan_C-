using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class VehiculeLocation
{
    public int IdVehiculeLoc { get; set; }

    public string Marque { get; set; } = null!;

    public string TypeVehicule { get; set; } = null!;

    public short NbSiege { get; set; }

    public string TypeConducteur { get; set; } = null!;

    public bool KillometreIllimite { get; set; }

    public byte[]? Img { get; set; }

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}

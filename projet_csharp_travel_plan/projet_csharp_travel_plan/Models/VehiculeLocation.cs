using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class VehiculeLocation
{
    public short IdVehiculeLoc { get; set; }

    public string Marque { get; set; } = null!;

    public string TypeVehicule { get; set; } = null!;

    public byte NbSiege { get; set; }

    public string TypeConducteur { get; set; } = null!;

    public bool KillometreIllimite { get; set; }

    public byte[]? Img { get; set; }

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}

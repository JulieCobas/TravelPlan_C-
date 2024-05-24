using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class DisponibiliteTransport
{
    public short IdSiege { get; set; }

    public short IdTransport { get; set; }

    public bool Disponible { get; set; }

    public virtual Siege IdSiegeNavigation { get; set; } = null!;

    public virtual Transport IdTransportNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class TransportOption
{
    public int IdOptionTransport { get; set; }

    public string? NumeroSiege { get; set; }

    public bool? BagageMain { get; set; }

    public bool? BagageLarge { get; set; }

    public bool? BagageEnSoute { get; set; }

    public bool? Speedyboarding { get; set; }

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}

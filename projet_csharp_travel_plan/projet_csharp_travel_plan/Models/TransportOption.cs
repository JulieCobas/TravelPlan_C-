using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class TransportOption
{
    public short IdOptionTransport { get; set; }

    public bool? BagageMain { get; set; }

    public int? PrixBagagemain { get; set; }

    public bool? BagageLarge { get; set; }

    public int? PrixBagagelarge { get; set; }

    public bool? BagageEnSoute { get; set; }

    public int? PrixBagagesoute { get; set; }

    public bool? Speedyboarding { get; set; }

    public int? PrixSpeedyboarding { get; set; }

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}

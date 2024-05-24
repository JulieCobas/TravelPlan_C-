using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Siege
{
    public short IdSiege { get; set; }

    public string NumeroSiege { get; set; } = null!;

    public virtual ICollection<DisponibiliteTransport> DisponibiliteTransports { get; set; } = new List<DisponibiliteTransport>();
}

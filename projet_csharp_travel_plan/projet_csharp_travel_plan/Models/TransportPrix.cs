using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class TransportPrix
{
    public short IdPrixTransport { get; set; }

    public DateTime DateDebutValidite { get; set; }

    public DateTime DateFinValidite { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();

    public virtual ICollection<CategoriePrix> IdCategoriePrixes { get; set; } = new List<CategoriePrix>();
}

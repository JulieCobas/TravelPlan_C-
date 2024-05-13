using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class TransportPrix
{
    [Key]
    public int IdPrixTransport { get; set; }

    public DateOnly DateDebutValidite { get; set; }

    public DateOnly DateFinValidite { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();

    public virtual ICollection<CategoriePrix> IdCategoriePrixes { get; set; } = new List<CategoriePrix>();
}

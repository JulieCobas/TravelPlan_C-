using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class TransportCategorie
{
    public int IdCategorieTransport { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}

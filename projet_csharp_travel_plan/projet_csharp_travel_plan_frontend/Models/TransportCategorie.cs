using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class TransportCategorie
{
    [Key]
    public int IdCategorieTransport { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}

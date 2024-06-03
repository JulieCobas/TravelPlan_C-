using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Pay
{
    public short IdPays { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Voyage> IdVoyages { get; set; } = new List<Voyage>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Voyage
{
    public short IdVoyage { get; set; }

    public short? IdClient { get; set; }

    public DateTime DateDebut { get; set; }

    public DateTime DateFin { get; set; }

    public decimal PrixTotal { get; set; }

    public bool StatutPaiement { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Pay> IdPays { get; set; } = new List<Pay>();
}

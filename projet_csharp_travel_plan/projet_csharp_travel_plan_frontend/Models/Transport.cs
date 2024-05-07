using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Transport
{
    public int IdTransport { get; set; }

    public int IdVehiculeLoc { get; set; }

    public int IdCategorieTransport { get; set; }

    public int IdPrixTransport { get; set; }

    public int? IdOptionTransport { get; set; }

    public int IdFournisseur { get; set; }

    public int IdPays { get; set; }

    public string LieuDepart { get; set; } = null!;

    public DateTime HeureDepart { get; set; }

    public DateTime HeureArrivee { get; set; }

    public short? Classe { get; set; }

    public virtual TransportCategorie IdCategorieTransportNavigation { get; set; } = null!;

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual TransportOption? IdOptionTransportNavigation { get; set; }

    public virtual Pay IdPaysNavigation { get; set; } = null!;

    public virtual TransportPrix IdPrixTransportNavigation { get; set; } = null!;

    public virtual VehiculeLocation IdVehiculeLocNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

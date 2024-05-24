using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Transport
{
    public short IdTransport { get; set; }

    public short IdVehiculeLoc { get; set; }

    public short IdCategorieTransport { get; set; }

    public short IdPrixTransport { get; set; }

    public short? IdOptionTransport { get; set; }

    public short IdFournisseur { get; set; }

    public short IdPays { get; set; }

    public string LieuDepart { get; set; } = null!;

    public DateTime HeureDepart { get; set; }

    public DateTime HeureArrivee { get; set; }

    public byte? Classe { get; set; }

    public virtual ICollection<DisponibiliteTransport> DisponibiliteTransports { get; set; } = new List<DisponibiliteTransport>();

    public virtual TransportCategorie IdCategorieTransportNavigation { get; set; } = null!;

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual TransportOption? IdOptionTransportNavigation { get; set; }

    public virtual Pay IdPaysNavigation { get; set; } = null!;

    public virtual TransportPrix IdPrixTransportNavigation { get; set; } = null!;

    public virtual VehiculeLocation IdVehiculeLocNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

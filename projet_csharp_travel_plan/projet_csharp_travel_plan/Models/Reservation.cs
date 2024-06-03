using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Reservation
{
    public short IdReservation { get; set; }

    public short? IdLogement { get; set; }

    public short? IdActivite { get; set; }

    public short? IdTransport { get; set; }

    public short IdVoyage { get; set; }

    public DateTime DateHeureDebut { get; set; }

    public DateTime? DateHeureFin { get; set; }

    public bool? Disponibilite { get; set; }

    public virtual Activite? IdActiviteNavigation { get; set; }

    public virtual Logement? IdLogementNavigation { get; set; }

    public virtual Transport? IdTransportNavigation { get; set; }

    public virtual Voyage IdVoyageNavigation { get; set; } = null!;

    public virtual ICollection<Invite> IdInvitees { get; set; } = new List<Invite>();
}

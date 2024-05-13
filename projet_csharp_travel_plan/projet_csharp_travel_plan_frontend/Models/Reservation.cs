using projet_csharp_travel_plan_frontend.Models;
using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Reservation
{
    public int IdReservation { get; set; }

    public int? IdLogement { get; set; }

    public int? IdActivite { get; set; }

    public int? IdTransport { get; set; }

    public int IdVoyage { get; set; }

    public DateOnly DateHeureDebut { get; set; }

    public DateOnly? DateHeureFin { get; set; }

    public virtual Activite? IdActiviteNavigation { get; set; }

    public virtual Logement? IdLogementNavigation { get; set; }

    public virtual Transport? IdTransportNavigation { get; set; }

    public virtual Voyage IdVoyageNavigation { get; set; } = null!;

    public virtual ICollection<Invite> IdInvitees { get; set; } = new List<Invite>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Invite
{
    public short IdInvitee { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public DateTime DateNaissance { get; set; }

    public string? Mail { get; set; }

    public virtual ICollection<Aspnetuser> Aspnetusers { get; set; } = new List<Aspnetuser>();

    public virtual ICollection<Reservation> IdReservations { get; set; } = new List<Reservation>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Invite
{
    public int IdInvitee { get; set; }

    public int IdUtilisateur { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public DateOnly DateNaissance { get; set; }

    public string? Mail { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Reservation> IdReservations { get; set; } = new List<Reservation>();
}

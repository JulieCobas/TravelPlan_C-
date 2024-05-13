using projet_csharp_travel_plan_frontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class Invite
{
    [Key]
    public int IdInvitee { get; set; }

    public int IdUtilisateur { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public DateOnly DateNaissance { get; set; }

    public string? Mail { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Reservation> IdReservations { get; set; } = new List<Reservation>();
}

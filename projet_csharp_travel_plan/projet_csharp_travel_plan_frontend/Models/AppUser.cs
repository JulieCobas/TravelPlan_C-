using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class AppUser : IdentityUser
{
    public int? IdUtilisateur { get; set; }

    public int? IdInvitee { get; set; }

}

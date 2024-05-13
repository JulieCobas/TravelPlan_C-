using projet_csharp_travel_plan_frontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class AspNetUserClaim
{
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}

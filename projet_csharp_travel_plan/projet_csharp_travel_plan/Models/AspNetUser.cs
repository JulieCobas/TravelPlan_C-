using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Aspnetuser
{
    public string Id { get; set; } = null!;

    public short? IdInvitee { get; set; }

    public string? Username { get; set; }

    public string? Normalizedusername { get; set; }

    public string? Email { get; set; }

    public string? Normalizedemail { get; set; }

    public bool Emailconfirmed { get; set; }

    public string? Passwordhash { get; set; }

    public string? Securitystamp { get; set; }

    public string? Concurrencystamp { get; set; }

    public string? Phonenumber { get; set; }

    public bool Phonenumberconfirmed { get; set; }

    public bool Twofactorenabled { get; set; }

    public DateTimeOffset? Lockoutend { get; set; }

    public bool Lockoutenabled { get; set; }

    public int Accessfailedcount { get; set; }

    public virtual ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; } = new List<Aspnetuserclaim>();

    public virtual ICollection<Aspnetuserlogin> Aspnetuserlogins { get; set; } = new List<Aspnetuserlogin>();

    public virtual ICollection<Aspnetusertoken> Aspnetusertokens { get; set; } = new List<Aspnetusertoken>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual Invite? IdInviteeNavigation { get; set; }

    public virtual ICollection<Aspnetrole> Roles { get; set; } = new List<Aspnetrole>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Paiement
{
    public short IdPaiement { get; set; }

    public short? IdClient { get; set; }

    public short IdUtilisateur { get; set; }

    public string TypeCarteBancaire { get; set; } = null!;

    public short NumeroCarteBancaire { get; set; }

    public DateTime DateExpiration { get; set; }

    public byte Crypto { get; set; }

    public virtual Client? IdClientNavigation { get; set; }
}

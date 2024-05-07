using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Paiement
{
    public int IdPaiement { get; set; }

    public int IdUtilisateur { get; set; }

    public string TypeCarteBancaire { get; set; } = null!;

    public int NumeroCarteBancaire { get; set; }

    public DateOnly DateExpiration { get; set; }

    public short Crypto { get; set; }

    public virtual Client IdUtilisateurNavigation { get; set; } = null!;
}

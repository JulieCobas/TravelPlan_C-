using projet_csharp_travel_plan.Models;
using System;
using System.Collections.Generic;
namespace projet_csharp_travel_plan_frontend.Models;

public partial class Voyage
{
    public int IdVoyage { get; set; }

    public int IdUtilisateur { get; set; }

    public DateOnly DateDebut { get; set; }

    public DateOnly DateFin { get; set; }

    public decimal PrixTotal { get; set; }

    public bool StatutPaiement { get; set; }

    public virtual Client IdUtilisateurNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Pay> IdPays { get; set; } = new List<Pay>();
}

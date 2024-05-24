using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Activite
{
    public short IdActivite { get; set; }

    public short? IdOptionActivite { get; set; }

    public short IdPrixActivite { get; set; }

    public short IdPays { get; set; }

    public short IdFournisseur { get; set; }

    public short IdCatActiv { get; set; }

    public string Nom { get; set; } = null!;

    public string Details { get; set; } = null!;

    public byte? Note { get; set; }

    public short? NbEvaluation { get; set; }

    public DateTime? HeuresMoyennes { get; set; }

    public byte[]? Img { get; set; }

    public int? CapaciteMax { get; set; }

    public virtual ActiviteCategorie IdCatActivNavigation { get; set; } = null!;

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual ActiviteOption? IdOptionActiviteNavigation { get; set; }

    public virtual Pay IdPaysNavigation { get; set; } = null!;

    public virtual ActivitePrix IdPrixActiviteNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

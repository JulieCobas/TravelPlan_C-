using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class Activite
{
    [Key]
    public int IdActivite { get; set; }

    public int? IdOptionActivite { get; set; }

    public int IdPrixActivite { get; set; }

    public int IdPays { get; set; }

    public int IdFournisseur { get; set; }

    public int IdCatActiv { get; set; }

    public string Nom { get; set; } = null!;

    public string Details { get; set; } = null!;

    public short? Note { get; set; }

    public int? NbEvaluation { get; set; }

    public TimeOnly? HeuresMoyennes { get; set; }

    public byte[] Img { get; set; } = null!;

    public virtual ActiviteCategorie IdCatActivNavigation { get; set; } = null!;

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual ActiviteOption? IdOptionActiviteNavigation { get; set; }

    public virtual Pay IdPaysNavigation { get; set; } = null!;

    public virtual ActivitePrix IdPrixActiviteNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

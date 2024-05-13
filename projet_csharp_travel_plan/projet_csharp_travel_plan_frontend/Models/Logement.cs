using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class Logement
{
    [Key]
    public int IdLogement { get; set; }

    public int IdLogementLoc { get; set; }

    public int IdFournisseur { get; set; }

    public int IdLogementCategorie { get; set; }

    public int IdPays { get; set; }

    public string Nom { get; set; } = null!;

    public string Details { get; set; } = null!;

    public short? Note { get; set; }

    public int? NbEvaluation { get; set; }

    public byte[] Img { get; set; } = null!;

    public virtual ICollection<Chambre> Chambres { get; set; } = new List<Chambre>();

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual LogementCategorie IdLogementCategorieNavigation { get; set; } = null!;

    public virtual LocationLogement IdLogementLocNavigation { get; set; } = null!;

    public virtual Pay IdPaysNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<EquipementCategorie> IdCatEquipements { get; set; } = new List<EquipementCategorie>();
}

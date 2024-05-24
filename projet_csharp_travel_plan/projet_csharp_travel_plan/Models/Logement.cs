using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Logement
{
    public short IdLogement { get; set; }

    public short IdFournisseur { get; set; }

    public short IdLogementPrix { get; set; }

    public short IdLogementCategorie { get; set; }

    public short IdPays { get; set; }

    public string Nom { get; set; } = null!;

    public string Details { get; set; } = null!;

    public byte? Note { get; set; }

    public int? NbEvaluation { get; set; }

    public byte[]? Img { get; set; }

    public bool? Disponibilite { get; set; }

    public virtual ICollection<Chambre> Chambres { get; set; } = new List<Chambre>();

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual LogementCategorie IdLogementCategorieNavigation { get; set; } = null!;

    public virtual PrixLogement IdLogementPrixNavigation { get; set; } = null!;

    public virtual Pay IdPaysNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<EquipementCategorie> IdCatEquipements { get; set; } = new List<EquipementCategorie>();
}

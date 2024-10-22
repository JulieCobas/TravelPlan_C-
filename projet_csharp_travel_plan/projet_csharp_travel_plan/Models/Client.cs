﻿using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Client
{
    public int IdUtilisateur { get; set; }

    public string Addresse { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Ville { get; set; } = null!;

    public string Pays { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public DateOnly DateNaissance { get; set; }

    public string MotDePasse { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string? Telephone { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();

    public virtual ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
}

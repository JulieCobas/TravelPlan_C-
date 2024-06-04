﻿using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class Pay
{
    public short IdPays { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();

    public virtual ICollection<Logement> Logements { get; set; } = new List<Logement>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();

    public virtual ICollection<Voyage> IdVoyages { get; set; } = new List<Voyage>();
}

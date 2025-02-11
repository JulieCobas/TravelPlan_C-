﻿using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class ActiviteOption
{
    public int IdOptionActivite { get; set; }

    public bool? EquipementInclu { get; set; }

    public bool? GuideAudio { get; set; }

    public bool? VisiteGuidee { get; set; }

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();
}

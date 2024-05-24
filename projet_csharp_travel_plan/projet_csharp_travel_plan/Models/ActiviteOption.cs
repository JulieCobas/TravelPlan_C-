using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models;

public partial class ActiviteOption
{
    public short IdOptionActivite { get; set; }

    public bool? GuideAudio { get; set; }

    public int? PrixGuideAudio { get; set; }

    public bool? VisiteGuidee { get; set; }

    public int? PrixVisiteGuide { get; set; }

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();
}

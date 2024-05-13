using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class ActiviteOption
{
    [Key]
    public int IdOptionActivite { get; set; }

    public bool? EquipementInclu { get; set; }

    public bool? GuideAudio { get; set; }

    public bool? VisiteGuidee { get; set; }

    public virtual ICollection<Activite> Activites { get; set; } = new List<Activite>();
}

using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan.Models
{
    public partial class Pay
    {
        public Pay()
        {
            Activites = new HashSet<Activite>();
            Logements = new HashSet<Logement>();
            Regions = new HashSet<Region>();
            Transports = new HashSet<Transport>();
            IdVoyages = new HashSet<Voyage>();
        }

        public short IdPays { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Activite> Activites { get; set; }
        public virtual ICollection<Logement> Logements { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<Transport> Transports { get; set; }
        public virtual ICollection<Voyage> IdVoyages { get; set; }
    }
}

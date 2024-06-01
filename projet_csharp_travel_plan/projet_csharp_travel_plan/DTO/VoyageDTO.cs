﻿

namespace projet_csharp_travel_plan.DTO
{
    public class VoyageDTO
    {
        public short IdVoyage { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public List<string> Pays { get; set; } // This assumes Pays is directly related to Voyage
    }
}

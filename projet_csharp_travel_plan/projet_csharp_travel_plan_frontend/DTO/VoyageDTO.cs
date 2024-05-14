using System;
using System.Collections.Generic;

namespace projet_csharp_travel_plan_frontend.DTO
{
    public class VoyageDTO
    {
        public DateOnly DateDebut { get; set; }
        public DateOnly DateFin { get; set; }
        public List<PayDTO> Pays { get; set; }
    }
}

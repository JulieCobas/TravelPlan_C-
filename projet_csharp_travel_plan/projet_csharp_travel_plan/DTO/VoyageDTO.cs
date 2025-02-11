﻿using System;
using System.Collections.Generic;
using projet_csharp_travel_plan_frontend.DTO;

namespace projet_csharp_travel_plan.DTO
{
    public class VoyageDTO
    {
        public DateOnly DateDebut { get; set; }
        public DateOnly DateFin { get; set; }
        public List<PayDTO> Pays { get; set; } = new List<PayDTO>(); // Initialiser la liste des pays
    }
}
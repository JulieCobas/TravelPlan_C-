﻿using projet_csharp_travel_plan.Models;

namespace projet_csharp_travel_plan.DTO
{
    public class TransportDTO
    {
        public short IdTransport { get; set; }
        public string NomFournisseur { get; set; }
        public short IdPays { get; set; }
        public string NomPays { get; set; } = null!;
        public decimal Prix { get; set; }
        public string CategorieTransportNom { get; set; }
        public string VehiculeLocMarque { get; set; }
        public string VehiculeLocTypeVehicule { get; set; }
        public byte VehiculeLocNbSiege { get; set; }

        // Transport option properties
        public bool? BagageMain { get; set; }
        public int? PrixBagageMain { get; set; }
        public bool? BagageEnSoute { get; set; }
        public int? PrixBagageEnSoute { get; set; }
        public bool? BagageLarge { get; set; }
        public int? PrixBagageLarge { get; set; }
        public bool? Speedyboarding { get; set; }
        public int? PrixSpeedyBoarding { get; set; }
        public string LieuDepart {  get; set; }

        // Transport category
        public TransportCategorieDTO CategorieTransport { get; set; }
    }
}

namespace projet_csharp_travel_plan.DTO
{
    public class TransportDTO
    {
        public short IdTransport { get; set; }
        public string NomFournisseur { get; set; }
        public bool? BagageMain { get; set; }
        public bool? BagageEnSoute { get; set; }
        public bool? BagageLarge { get; set; }
        public bool? Speedyboarding { get; set; }
        public decimal Prix { get; set; }
        public string CategorieTransportNom { get; set; }
        public string VehiculeLocMarque { get; set; }
        public string VehiculeLocTypeVehicule { get; set; }
        public byte VehiculeLocNbSiege { get; set; }

        // OptionTransport properties
        public bool OptionTransportBagageMain { get; set; }
        public bool OptionTransportBagageEnSoute { get; set; }
        public bool OptionTransportBagageLarge { get; set; }
        public bool OptionTransportSpeedyboarding { get; set; }

        // Add more properties if needed
    }
}

namespace projet_csharp_travel_plan_frontend.DTO
{
    public class TransportDto
    {
        public int IdTransport { get; set; }

        public int IdVehiculeLoc { get; set; }

        public int IdCategorieTransport { get; set; }

        public int IdPrixTransport { get; set; }

        public int? IdOptionTransport { get; set; }

        public int IdFournisseur { get; set; }

        public int IdPays { get; set; }

        public string LieuDepart { get; set; } = null!;

        public DateTime HeureDepart { get; set; }

        public DateTime HeureArrivee { get; set; }

        public short? Classe { get; set; }

        // Optionally include simple properties from navigation properties
        public string? CategorieTransportNom { get; set; }
        public string? FournisseurNomCompagnie { get; set; }
        public string? VehiculeLocMarque { get; set; }
        public string? VehiculeLocTypeVehicule { get; set; }
        public int? VehiculeLocNbSiege { get; set; }
        public string? OptionTransportNumeroSiege { get; set; }
        public bool? OptionTransportBagageMain { get; set; }
        public bool? OptionTransportBagageEnSoute { get; set; }
        public bool? OptionTransportBagageLarge { get; set; }
        public bool? OptionTransportSpeedyboarding { get; set; }
        public decimal? PrixTransportPrix { get; set; }
    }
}

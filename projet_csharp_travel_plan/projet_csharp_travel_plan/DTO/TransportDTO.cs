namespace projet_csharp_travel_plan.DTO
{
    public class TransportDTO
    {
        public short IdTransport { get; set; }
        public short IdVehiculeLoc { get; set; }
        public short IdCategorieTransport { get; set; }
        public short IdPrixTransport { get; set; }
        public short? IdOptionTransport { get; set; }
        public short IdFournisseur { get; set; }
        public short IdPays { get; set; }
        public string LieuDepart { get; set; }
        public DateTime HeureDepart { get; set; }
        public DateTime HeureArrivee { get; set; }
        public byte? Classe { get; set; }

        // Navigation properties
        public string NomCategorie { get; set; }
        public string NomFournisseur { get; set; }
        public string NomPays { get; set; }
        public string MarqueVehicule { get; set; }
        public string TypeVehicule { get; set; }
        public int NbSiegesVehicule { get; set; }
        public decimal PrixTransport { get; set; }
        public bool? OptionBagageMain { get; set; }
        public bool? OptionBagageEnSoute { get; set; }
        public bool? OptionBagageLarge { get; set; }
        public bool? OptionSpeedyboarding { get; set; }
        public int? NumeroSiege { get; set; }
    }
}

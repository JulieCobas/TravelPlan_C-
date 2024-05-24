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
        public string LieuDepart { get; set; } = null!;
        public DateTime HeureDepart { get; set; }
        public DateTime HeureArrivee { get; set; }
        public byte? Classe { get; set; }

        // Navigation properties
        public TransportCategorieDTO CategorieTransport { get; set; }
        public FournisseurDTO Fournisseur { get; set; }
        public TransportOptionDTO? OptionTransport { get; set; }
        public PayDTO Pays { get; set; }
        public TransportPrixDTO PrixTransport { get; set; }
        public VehiculeLocationDTO VehiculeLoc { get; set; }
        public List<DisponibiliteTransportDTO> DisponibiliteTransports { get; set; } = new List<DisponibiliteTransportDTO>();
        public List<ReservationDTO> Reservations { get; set; } = new List<ReservationDTO>();
    }
}

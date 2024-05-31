namespace projet_csharp_travel_plan_frontend.DTO
{
    public class ReservationDTO
    {
        public short IdReservation { get; set; }
        public short? IdLogement { get; set; }
        public short? IdActivite { get; set; }
        public short? IdTransport { get; set; }
        public short IdVoyage { get; set; }
        public DateTime DateHeureDebut { get; set; }
        public DateTime? DateHeureFin { get; set; }
        public bool? Disponibilite { get; set; }

        // Navigation properties
        public LogementDTO? Logement { get; set; }
        public ActiviteDTO? Activite { get; set; }
        public TransportDTO? Transport { get; set; }
        public VoyageDTO Voyage { get; set; }
    }
}

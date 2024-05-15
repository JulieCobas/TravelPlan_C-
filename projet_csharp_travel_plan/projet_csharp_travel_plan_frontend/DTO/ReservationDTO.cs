namespace projet_csharp_travel_plan_frontend.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int IdVoyage { get; set; }
        public int? IdLogement { get; set; }
        public int? IdActivite { get; set; }
        public int? IdTransport { get; set; }
        public DateOnly DateHeureDebut { get; set; }
        public DateOnly? DateHeureFin { get; set; }
        public LogementDTO Logement { get; set; }
        public ActiviteDTO Activite { get; set; }
        public TransportDto Transport { get; set; }
    }
}

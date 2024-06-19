namespace projet_csharp_travel_plan_frontend.DTO
{
    public class ReservationPaysModelDTO
    {
        public short IdReservation { get; set; }
        public short? IdLogement { get; set; }
        public short? IdActivite { get; set; }
        public short? IdTransport { get; set; }
        public short IdVoyage { get; set; }
        public VoyageDTO? Voyage { get; set; }
        public string NomPays {  get; set; }    
        public DateTime DateHeureDebut { get; set; }
        public DateTime? DateHeureFin { get; set; }

        // Propriétés calculées pour la date
        public DateTime DateDebut => DateHeureDebut.Date;
        public DateTime? DateFin => DateHeureFin?.Date;

        // Propriétés calculées pour l'heure
        public TimeSpan HeureDebut => DateHeureDebut.TimeOfDay;
        public TimeSpan? HeureFin => DateHeureFin?.TimeOfDay;

        public bool? Disponibilite { get; set; }
        public LogementDTO? Logement { get; set; }
        public ActiviteDTO? Activite { get; set; }
        public TransportDTO? Transport { get; set; }
    }
}

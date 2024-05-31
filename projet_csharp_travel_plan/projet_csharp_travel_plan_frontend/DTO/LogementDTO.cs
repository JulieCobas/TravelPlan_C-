namespace projet_csharp_travel_plan_frontend.DTO
{
    public class LogementDTO
    {
        public short IdLogement { get; set; }
        public short IdFournisseur { get; set; }
        public short IdLogementCategorie { get; set; }
        public short IdLogementPrix { get; set; }
        public short IdPays { get; set; }
        public string Nom { get; set; } = null!;
        public string Details { get; set; } = null!;
        public byte? Note { get; set; }
        public int? NbEvaluation { get; set; }
        public byte[] Img { get; set; }
        public bool? Disponibilite { get; set; }

        // Navigation properties
        public string NomFournisseur { get; set; }
        public string NomCategorie { get; set; }
        public string NomPays { get; set; }
        public decimal PrixLogement { get; set; }
        public List<ReservationDTO> Reservations { get; set; } = new List<ReservationDTO>();
    }
}

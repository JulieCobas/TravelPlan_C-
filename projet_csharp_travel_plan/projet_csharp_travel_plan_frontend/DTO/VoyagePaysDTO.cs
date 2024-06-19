namespace projet_csharp_travel_plan_frontend.DTO
{
    public class VoyagePaysDTO
    {
        public int IdVoyage { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int IdClient { get; set; }
        public decimal PrixTotal { get; set; }
        public string NomPays { get; set; } // Nom du pays directement inclus
    }
}

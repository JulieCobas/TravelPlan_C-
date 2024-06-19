namespace projet_csharp_travel_plan.DTO
{
    public class VoyageDTO
    {
        public int IdVoyage { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public short IdClient { get; set; }
        public decimal PrixTotal { get; set; }
        public short IdPays { get; set; }
    }
}
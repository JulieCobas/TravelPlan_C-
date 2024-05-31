namespace projet_csharp_travel_plan_frontend.DTO
{
    public class VoyageDTO
    {
        public int IdVoyage { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal PrixTotal { get; set; }
        public bool StatutPaiement { get; set; }
        public ClientDTO Client { get; set; }
    }
}

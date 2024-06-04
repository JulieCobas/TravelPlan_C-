namespace projet_csharp_travel_plan_frontend.DTO
{
    public class VoyageDTO
    {
        public short IdVoyage { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public List<PayDTO> Pays { get; set; } // This assumes Pays is directly related to Voyage

        public short IdPays {  get; set; }
    }
}

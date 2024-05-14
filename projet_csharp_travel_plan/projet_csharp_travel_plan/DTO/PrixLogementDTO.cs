namespace projet_csharp_travel_plan.DTO
{
    public class PrixLogementDTO
    {
        public int IdLogementPrix { get; set; }
        public DateTime DateDebutValidite { get; set; }
        public DateTime DateFinValidite { get; set; }
        public decimal Prix { get; set; }
    }
}

namespace projet_csharp_travel_plan.DTO
{
    public class PrixLogementDTO
    {
        public short IdLogementPrix { get; set; }
        public DateTime DateDebutValidite { get; set; }
        public DateTime DateFinValidite { get; set; }
        public decimal Prix { get; set; }

        // Navigation properties
        public List<LogementDTO> Logements { get; set; } = new List<LogementDTO>();
    }
}

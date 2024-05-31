namespace projet_csharp_travel_plan.DTO
{
    public class LogementCategorieDTO
    {
        public short IdLogementCategorie { get; set; }
        public string Nom { get; set; } = null!;

        // Navigation properties
        public List<LogementDTO> Logements { get; set; } = new List<LogementDTO>();
    }
}

namespace projet_csharp_travel_plan.DTO
{
    public class PayDTO
    {
        public short IdPays { get; set; }
        public string Nom { get; set; }
        public List<string> Activites { get; set; } = new List<string>();
        public List<string> Logements { get; set; } = new List<string>();
        public List<string> Regions { get; set; } = new List<string>();
        public List<string> Transports { get; set; } = new List<string>();
        public List<string> Voyages { get; set; } = new List<string>();
    }
}

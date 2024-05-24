namespace projet_csharp_travel_plan_frontend.DTO
{
    public class RegionDTO
    {
        public short IdRegion { get; set; }
        public string Nom { get; set; }
        public short IdPays { get; set; }
        public List<string> Villes { get; set; }
    }
}
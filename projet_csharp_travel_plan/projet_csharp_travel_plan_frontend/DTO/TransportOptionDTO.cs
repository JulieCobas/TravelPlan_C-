namespace projet_csharp_travel_plan_frontend.DTO
{
    public class TransportOptionDTO
    {
        public short IdOptionTransport { get; set; }
        public bool? BagageMain { get; set; }
        public int? PrixBagageMain { get; set; }
        public bool? BagageLarge { get; set; }
        public int? PrixBagageLarge { get; set; }
        public bool? BagageEnSoute { get; set; }
        public int? PrixBagageEnSoute { get; set; }
        public bool? Speedyboarding { get; set; }
        public int? PrixSpeedyBoarding { get; set; }
        public int? NumeroSiege { get; set; }
        // Navigation properties
        public List<TransportDTO> Transports { get; set; } = new List<TransportDTO>();
    }
}

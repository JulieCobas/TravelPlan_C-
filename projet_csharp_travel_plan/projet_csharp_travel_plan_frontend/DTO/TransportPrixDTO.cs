namespace projet_csharp_travel_plan_frontend.DTO
{
    public class TransportPrixDTO
    {
        public short IdPrixTransport { get; set; }
        public DateTime DateDebutValidite { get; set; }
        public DateTime DateFinValidite { get; set; }
        public decimal Prix { get; set; }

        // Navigation properties
        public List<TransportDTO> Transports { get; set; } = new List<TransportDTO>();
    }
}

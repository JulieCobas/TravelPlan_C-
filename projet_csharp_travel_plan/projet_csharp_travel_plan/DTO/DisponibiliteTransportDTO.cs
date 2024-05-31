namespace projet_csharp_travel_plan.DTO
{
    public class DisponibiliteTransportDTO
    {
        public short IdSiege { get; set; }
        public short IdTransport { get; set; }
        public bool Disponible { get; set; }

        // Navigation properties
        public SiegeDTO Siege { get; set; }
        public TransportDTO Transport { get; set; }
    }
}

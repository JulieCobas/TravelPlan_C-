namespace projet_csharp_travel_plan.DTO
{
    public class SiegeDTO
    {
        public short IdSiege { get; set; }
        public string NumeroSiege { get; set; } = null!;

        // Navigation properties
        public List<DisponibiliteTransportDTO> DisponibiliteTransports { get; set; } = new List<DisponibiliteTransportDTO>();
    }
}

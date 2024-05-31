namespace projet_csharp_travel_plan.DTO
{
    public class TransportCategorieDTO
    {
        public short IdCategorieTransport { get; set; }
        public string Nom { get; set; } = null!;

        // Navigation properties
        public List<TransportDTO> Transports { get; set; } = new List<TransportDTO>();
    }
}

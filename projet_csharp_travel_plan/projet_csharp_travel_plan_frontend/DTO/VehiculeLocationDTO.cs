namespace projet_csharp_travel_plan_frontend.DTO
{
    public class VehiculeLocationDTO
    {
        public short IdVehiculeLoc { get; set; }
        public string Marque { get; set; } = null!;
        public string TypeVehicule { get; set; } = null!;
        public byte NbSiege { get; set; }
        public string TypeConducteur { get; set; } = null!;
        public bool KillometreIllimite { get; set; }
        public byte[] Img { get; set; }

        // Navigation properties
        public List<TransportDTO> Transports { get; set; } = new List<TransportDTO>();
    }
}

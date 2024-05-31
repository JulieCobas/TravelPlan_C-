namespace projet_csharp_travel_plan_frontend.DTO
{
    public class ChambreDTO
    {
        public short IdChambre { get; set; }
        public short IdLogementPrix { get; set; }
        public short IdLogement { get; set; }
        public short? IdChambreOption { get; set; }
        public string Nom { get; set; } = null!;
        public string? TypeDeChambre { get; set; }
        public byte Surface { get; set; }
        public byte NbOccupants { get; set; }
        public string? DetailsChambre { get; set; }

        // Navigation properties
        public string? LogementNom { get; set; }
        public string? LogementPrixNom { get; set; }
        public string? ChambreOptionDetails { get; set; }
        public List<DisponibiliteLogementDTO> DisponibiliteLogements { get; set; } = new List<DisponibiliteLogementDTO>();
        public List<ChambreEquipementDTO> EquipChambres { get; set; } = new List<ChambreEquipementDTO>();
    }
}

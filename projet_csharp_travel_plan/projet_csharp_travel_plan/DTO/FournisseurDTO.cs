namespace projet_csharp_travel_plan.DTO
{
    public class FournisseurDTO
    {
        public short IdFournisseur { get; set; }
        public string NomCompagnie { get; set; } = null!;
        public string Adresse { get; set; } = null!;
        public string Cp { get; set; } = null!;
        public string Ville { get; set; } = null!;
        public string Pays { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Telephone { get; set; }
        public string CompteBancaire { get; set; } = null!;

        // Navigation properties
        public List<TransportDTO> Transports { get; set; } = new List<TransportDTO>();
        public List<LogementDTO> Logements { get; set; } = new List<LogementDTO>();
        public List<ActiviteDTO> Activites { get; set; } = new List<ActiviteDTO>();
    }
}

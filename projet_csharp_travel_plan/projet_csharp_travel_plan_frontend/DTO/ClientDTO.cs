namespace projet_csharp_travel_plan_frontend.DTO
{
    public class ClientDTO
    {
        public short IdClient { get; set; }
        public string Addresse { get; set; } = null!;
        public string Cp { get; set; } = null!;
        public string Ville { get; set; } = null!;
        public string Pays { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public DateTime DateNaissance { get; set; }
        public string? Mail { get; set; }

        // Navigation properties
        public List<VoyageDTO> Voyages { get; set; } = new List<VoyageDTO>();
    }
}

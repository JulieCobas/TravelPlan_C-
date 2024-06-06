namespace projet_csharp_travel_plan.DTO
{
    public class ClientDTO
    {
        public short IdClient { get; set; }
        public string? Id { get; set; }
        public string Addresse { get; set; } = null!;
        public string Cp { get; set; } = null!;
        public string Ville { get; set; } = null!;
        public string Pays { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public DateOnly DateNaissance { get; set; }
    }
}

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
        public string? Telephone { get; set; }
        public string CompteBancaire { get; set; } = null!;

        // Optionally include collections if needed
        public ICollection<string> ActiviteNoms { get; set; } = new List<string>();
        public ICollection<string> LogementNoms { get; set; } = new List<string>();
        public ICollection<string> TransportNoms { get; set; } = new List<string>();
    }
}

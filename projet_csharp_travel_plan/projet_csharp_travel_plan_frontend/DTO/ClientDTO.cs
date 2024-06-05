using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.DTO
{
    public class ClientDTO
    {
        [Required]
        public short IdClient { get; set; }
        public string? Id { get; set; }
        [Required]
        public string Addresse { get; set; } 
        [Required]
        public string Cp { get; set; } 
        [Required]
        public string Ville { get; set; } 
        [Required]
        public string Pays { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public DateOnly DateNaissance { get; set; }

    }
}

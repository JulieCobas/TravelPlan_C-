namespace projet_csharp_travel_plan.DTO
{
    public class ActiviteDTO
    {
        public short IdActivite { get; set; }
        public short? IdOptionActivite { get; set; }
        public short IdPrixActivite { get; set; }
        public short IdPays { get; set; }
        public short IdFournisseur { get; set; }
        public short IdCatActiv { get; set; }
        public string Nom { get; set; } = null!;
        public string Details { get; set; } = null!;
        public byte? Note { get; set; }
        public short? NbEvaluation { get; set; }
        public DateTime? HeuresMoyennes { get; set; }
        public byte[]? Img { get; set; }
        public int? CapaciteMax { get; set; }

        // Navigation properties
        public string? CategorieActiviteNom { get; set; }
        public string? FournisseurNom { get; set; }
        public string? PaysNom { get; set; }
        public decimal? PrixActivite { get; set; }

        // Option properties
        public bool? GuideAudio { get; set; }
        public int? PrixGuideAudio { get; set; }
        public bool? VisiteGuidee { get; set; }
        public int? PrixVisiteGuide { get; set; }
    }
}

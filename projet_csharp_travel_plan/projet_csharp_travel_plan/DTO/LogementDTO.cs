namespace projet_csharp_travel_plan.DTO
{
    public class LogementDTO
    {
        public short IdLogement { get; set; }
        public string Nom { get; set; } = null!;
        public string Details { get; set; } = null!;
        public byte? Note { get; set; }
        public int? NbEvaluation { get; set; }
        public byte[]? Img { get; set; }
        public bool? Disponibilite { get; set; }
        public string NomFournisseur { get; set; } = null!;
        public string NomCategorie { get; set; } = null!;
        public string NomPays { get; set; } = null!;
    }
}

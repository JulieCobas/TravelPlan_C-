namespace projet_csharp_travel_plan_frontend.DTO
{
    public class LogementDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        // Autres propriétés du logement
        public string Details { get; set; }
        public short? Note { get; set; }
        public int? NbEvaluation { get; set; }
        // Ajoutez d'autres propriétés du logement selon vos besoins

        public string NomFournisseur { get; set; }
        public string NomCategorie { get; set; }
        public string NomPays { get; set; }
    }
}

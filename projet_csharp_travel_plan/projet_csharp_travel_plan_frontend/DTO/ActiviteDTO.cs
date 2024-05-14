using System;

namespace projet_csharp_travel_plan_frontend.DTO
{
    public class ActiviteDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Details { get; set; }
        public short? Note { get; set; }
        public int? NbEvaluation { get; set; }
        public TimeSpan? HeuresMoyennes { get; set; }
        // Ajoutez d'autres propriétés de l'activité selon vos besoins
        public string NomFournisseur { get; set; }
        public bool? EquipementInclu { get; set; }
        public bool? GuideAudio { get; set; }
        public bool? VisiteGuidee { get; set; }
        public string NomPays { get; set; }
        // Ajoutez la propriété Prix
        public decimal Prix { get; set; }
        public string NomCategorie { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_csharp_travel_plan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitesController : ControllerBase
    {
        private readonly TravelPlanContext _context;

        public ActivitesController(TravelPlanContext context)
        {
            _context = context;
        }

        // GET: api/Activites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiviteDTO>>> GetActivites()
        {
            var activites = await _context.Activites
                .Include(a => a.IdCatActivNavigation)
                .Include(a => a.IdFournisseurNavigation)
                .Include(a => a.IdOptionActiviteNavigation)
                .Include(a => a.IdPaysNavigation)
                .Include(a => a.IdPrixActiviteNavigation)
                .ToListAsync();

            return activites.Select(a => new ActiviteDTO
            {
                IdActivite = a.IdActivite,
                IdOptionActivite = a.IdOptionActivite,
                IdPrixActivite = a.IdPrixActivite,
                IdPays = a.IdPays,
                IdFournisseur = a.IdFournisseur,
                IdCatActiv = a.IdCatActiv,
                Nom = a.Nom,
                Details = a.Details,
                Note = a.Note,
                NbEvaluation = a.NbEvaluation,
                HeuresMoyennes = a.HeuresMoyennes,
                Img = a.Img,
                CapaciteMax = a.CapaciteMax,
                CategorieActiviteNom = a.IdCatActivNavigation?.Nom,
                FournisseurNom = a.IdFournisseurNavigation?.NomCompagnie,
                PaysNom = a.IdPaysNavigation?.Nom,
                PrixActivite = a.IdPrixActiviteNavigation?.Prix,
                GuideAudio = a.IdOptionActiviteNavigation?.GuideAudio,
                PrixGuideAudio = a.IdOptionActiviteNavigation?.PrixGuideAudio,
                VisiteGuidee = a.IdOptionActiviteNavigation?.VisiteGuidee,
                PrixVisiteGuide = a.IdOptionActiviteNavigation?.PrixVisiteGuide
            }).ToList();
        }

        // GET: api/Activites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActiviteDTO>> GetActivite(short id)
        {
            var activite = await _context.Activites
                .Include(a => a.IdCatActivNavigation)
                .Include(a => a.IdFournisseurNavigation)
                .Include(a => a.IdOptionActiviteNavigation)
                .Include(a => a.IdPaysNavigation)
                .Include(a => a.IdPrixActiviteNavigation)
                .FirstOrDefaultAsync(a => a.IdActivite == id);

            if (activite == null)
            {
                return NotFound();
            }

            var activiteDto = new ActiviteDTO
            {
                IdActivite = activite.IdActivite,
                IdOptionActivite = activite.IdOptionActivite,
                IdPrixActivite = activite.IdPrixActivite,
                IdPays = activite.IdPays,
                IdFournisseur = activite.IdFournisseur,
                IdCatActiv = activite.IdCatActiv,
                Nom = activite.Nom,
                Details = activite.Details,
                Note = activite.Note,
                NbEvaluation = activite.NbEvaluation,
                HeuresMoyennes = activite.HeuresMoyennes,
                Img = activite.Img,
                CapaciteMax = activite.CapaciteMax,
                CategorieActiviteNom = activite.IdCatActivNavigation?.Nom,
                FournisseurNom = activite.IdFournisseurNavigation?.NomCompagnie,
                PaysNom = activite.IdPaysNavigation?.Nom,
                PrixActivite = activite.IdPrixActiviteNavigation?.Prix,
                GuideAudio = activite.IdOptionActiviteNavigation?.GuideAudio,
                PrixGuideAudio = activite.IdOptionActiviteNavigation?.PrixGuideAudio,
                VisiteGuidee = activite.IdOptionActiviteNavigation?.VisiteGuidee,
                PrixVisiteGuide = activite.IdOptionActiviteNavigation?.PrixVisiteGuide
            };

            return activiteDto;
        }
    }
}

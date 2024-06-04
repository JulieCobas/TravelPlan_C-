using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ActivitesController : ControllerBase
{
    private readonly TravelPlanNewDbContext _context;

    public ActivitesController(TravelPlanNewDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActiviteDTO>>> GetActivites()
    {
        var activites = await _context.Activites
            .Include(a => a.IdFournisseurNavigation)
            .Include(a => a.IdCatActivNavigation)
            .Include(a => a.IdPaysNavigation)
            .Include(a => a.IdPrixActiviteNavigation)
            .Select(a => new ActiviteDTO
            {
                IdActivite = a.IdActivite,
                Nom = a.Nom,
                Details = a.Details,
                Note = a.Note,
                NbEvaluation = a.NbEvaluation,
                HeuresMoyennes = a.HeuresMoyennes,
                Img = a.Img,
                CapaciteMax = a.CapaciteMax,
                NomCategorie = a.IdCatActivNavigation.Nom,
                NomFournisseur = a.IdFournisseurNavigation.NomCompagnie,
                NomPays = a.IdPaysNavigation.Nom,
                PrixActivite = a.IdPrixActiviteNavigation.Prix,
                GuideAudio = a.IdOptionActiviteNavigation.GuideAudio,
                PrixGuideAudio = a.IdOptionActiviteNavigation.PrixGuideAudio,
                VisiteGuidee = a.IdOptionActiviteNavigation.VisiteGuidee,
                PrixVisiteGuide = a.IdOptionActiviteNavigation.PrixVisiteGuide
            })
            .ToListAsync();

        return activites;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActiviteDTO>> GetActivite(int id)
    {
        var activite = await _context.Activites
            .Include(a => a.IdFournisseurNavigation)
            .Include(a => a.IdCatActivNavigation)
            .Include(a => a.IdPaysNavigation)
            .Include(a => a.IdPrixActiviteNavigation)
            .Where(a => a.IdActivite == id)
            .Select(a => new ActiviteDTO
            {
                IdActivite = a.IdActivite,
                Nom = a.Nom,
                Details = a.Details,
                Note = a.Note,
                NbEvaluation = a.NbEvaluation,
                HeuresMoyennes = a.HeuresMoyennes,
                Img = a.Img,
                CapaciteMax = a.CapaciteMax,
                NomCategorie = a.IdCatActivNavigation.Nom,
                NomFournisseur = a.IdFournisseurNavigation.NomCompagnie,
                NomPays = a.IdPaysNavigation.Nom,
                PrixActivite = a.IdPrixActiviteNavigation.Prix,
                GuideAudio = a.IdOptionActiviteNavigation.GuideAudio,
                PrixGuideAudio = a.IdOptionActiviteNavigation.PrixGuideAudio,
                VisiteGuidee = a.IdOptionActiviteNavigation.VisiteGuidee,
                PrixVisiteGuide = a.IdOptionActiviteNavigation.PrixVisiteGuide
            })
            .FirstOrDefaultAsync();

        if (activite == null)
        {
            return NotFound();
        }

        return activite;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivite(int id, ActiviteDTO dto)
    {
        if (id != dto.IdActivite)
        {
            return BadRequest();
        }

        var activite = await _context.Activites.FindAsync(id);
        if (activite == null)
        {
            return NotFound();
        }

        activite.Nom = dto.Nom;
        activite.Details = dto.Details;
        activite.Note = dto.Note;
        activite.NbEvaluation = dto.NbEvaluation;
        activite.HeuresMoyennes = dto.HeuresMoyennes;
        activite.Img = dto.Img;
        activite.CapaciteMax = dto.CapaciteMax;
        // Update other fields if necessary

        _context.Entry(activite).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActiviteExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool ActiviteExists(int id)
    {
        return _context.Activites.Any(e => e.IdActivite == id);
    }
}

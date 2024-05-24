using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;

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
        public async Task<ActionResult<IEnumerable<ActiviteDTO>>> GetActivites([FromQuery] string country = null)
        {
            var query = _context.Activites
                .Include(a => a.IdCatActivNavigation)
                .Include(a => a.IdFournisseurNavigation)
                .Include(a => a.IdOptionActiviteNavigation)
                .Include(a => a.IdPaysNavigation)
                .Include(a => a.IdPrixActiviteNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(a => a.IdPaysNavigation.Nom == country);
            }

            var activites = await query
                .Select(a => new ActiviteDTO
                {
                    Id = a.IdActivite,
                    Nom = a.Nom,
                    Details = a.Details,
                    Note = a.Note,
                    NbEvaluation = a.NbEvaluation,
                    HeuresMoyennes = a.HeuresMoyennes.HasValue ? TimeSpan.FromHours(a.HeuresMoyennes.Value.Hour) : (TimeSpan?)null,
                    NomFournisseur = a.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = a.IdCatActivNavigation.Nom,
                    EquipementInclu = a.IdOptionActiviteNavigation.EquipementInclu,
                    GuideAudio = a.IdOptionActiviteNavigation.GuideAudio,
                    VisiteGuidee = a.IdOptionActiviteNavigation.VisiteGuidee,
                    NomPays = a.IdPaysNavigation.Nom,
                    Prix = a.IdPrixActiviteNavigation.Prix
                })
                .ToListAsync();

            return activites;
        }


        // GET: api/Activites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activite>> GetActivite(int id)
        {
            var activite = await _context.Activites.FindAsync(id);

            if (activite == null)
            {
                return NotFound();
            }

            return activite;
        }

        // PUT: api/Activites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivite(int id, Activite activite)
        {
            if (id != activite.IdActivite)
            {
                return BadRequest();
            }

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

        // POST: api/Activites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activite>> PostActivite(Activite activite)
        {
            _context.Activites.Add(activite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivite", new { id = activite.IdActivite }, activite);
        }

        // DELETE: api/Activites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivite(int id)
        {
            var activite = await _context.Activites.FindAsync(id);
            if (activite == null)
            {
                return NotFound();
            }

            _context.Activites.Remove(activite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActiviteExists(int id)
        {
            return _context.Activites.Any(e => e.IdActivite == id);
        }
    }
}

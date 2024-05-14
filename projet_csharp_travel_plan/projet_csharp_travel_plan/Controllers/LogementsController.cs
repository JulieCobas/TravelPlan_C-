using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.Models;

namespace projet_csharp_travel_plan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogementsController : ControllerBase
    {
        private readonly TravelPlanContext _context;

        public LogementsController(TravelPlanContext context)
        {
            _context = context;
        }

        // GET: api/Logements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logement>>> GetLogements(int? idPays, bool includeFournisseurs = false, bool includeCategories = false, bool includePays = false)
        {
            IQueryable<Logement> query = _context.Logements;

            // Vérifie si l'ID du pays est fourni
            if (idPays != null)
            {
                query = query.Where(l => l.IdPays == idPays);
            }

            // Inclure les données associées des fournisseurs, des catégories de logements et des pays si les paramètres sont définis à true
            if (includeFournisseurs)
            {
                query = query.Include(l => l.IdFournisseurNavigation);
            }
            if (includeCategories)
            {
                query = query.Include(l => l.IdLogementCategorieNavigation);
            }
            if (includePays)
            {
                query = query.Include(l => l.IdPaysNavigation);
            }

            return await query.ToListAsync();
        }


        // PUT: api/Logements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogement(int id, Logement logement)
        {
            if (id != logement.IdLogement)
            {
                return BadRequest();
            }

            _context.Entry(logement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogementExists(id))
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

        // POST: api/Logements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Logement>> PostLogement(Logement logement)
        {
            _context.Logements.Add(logement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogement", new { id = logement.IdLogement }, logement);
        }

        // DELETE: api/Logements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogement(int id)
        {
            var logement = await _context.Logements.FindAsync(id);
            if (logement == null)
            {
                return NotFound();
            }

            _context.Logements.Remove(logement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogementExists(int id)
        {
            return _context.Logements.Any(e => e.IdLogement == id);
        }
    }
}

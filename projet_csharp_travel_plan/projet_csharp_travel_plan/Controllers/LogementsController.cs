﻿using System;
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
    public class LogementsController : ControllerBase
    {
        private readonly TravelPlanContext _context;

        public LogementsController(TravelPlanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogementDTO>>> GetLogements()
        {
            var logements = await _context.Logements
                .Include(l => l.IdFournisseurNavigation)
                .Include(l => l.IdLogementCategorieNavigation)
                .Include(l => l.IdPaysNavigation)
                .Select(l => new LogementDTO
                {
                    Id = l.IdLogement,
                    Nom = l.Nom,
                    // Autres propriétés du logement
                    Details = l.Details,
                    Note = l.Note,
                    NbEvaluation = l.NbEvaluation,
                    // Ajoutez d'autres propriétés du logement selon vos besoins

                    NomFournisseur = l.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = l.IdLogementCategorieNavigation.Nom,
                    NomPays = l.IdPaysNavigation.Nom
                })
                .ToListAsync();

            return logements;
        }



        // GET: api/Logements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogementDTO>> GetLogement(int id)
        {
            var logement = await _context.Logements
                .Include(l => l.IdFournisseurNavigation)
                .Include(l => l.IdLogementCategorieNavigation)
                .Include(l => l.IdPaysNavigation)
                .Where(l => l.IdLogement == id)
                .Select(l => new LogementDTO
                {
                    Id = l.IdLogement,
                    Nom = l.Nom,
                    // Autres propriétés du logement
                    Details = l.Details,
                    Note = l.Note,
                    NbEvaluation = l.NbEvaluation,
                    // Ajoutez d'autres propriétés du logement selon vos besoins

                    NomFournisseur = l.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = l.IdLogementCategorieNavigation.Nom,
                    NomPays = l.IdPaysNavigation.Nom
                })
                .FirstOrDefaultAsync();

            if (logement == null)
            {
                return NotFound();
            }

            return logement;
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

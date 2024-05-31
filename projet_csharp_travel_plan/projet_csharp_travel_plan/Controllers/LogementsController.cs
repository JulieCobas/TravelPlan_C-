﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.Models;
using projet_csharp_travel_plan.DTO;

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
        public async Task<ActionResult<IEnumerable<LogementDTO>>> GetLogements([FromQuery] string country)
        {
            var logements = await _context.Logements
                .Include(l => l.IdFournisseurNavigation)
                .Include(l => l.IdLogementCategorieNavigation)
                .Include(l => l.IdPaysNavigation)
                .Include(l => l.IdLogementPrixNavigation)
                .Where(l => l.IdPaysNavigation.Nom == country)
                .Select(l => new LogementDTO
                {
                    IdLogement = l.IdLogement,
                    Nom = l.Nom,
                    Details = l.Details,
                    Note = l.Note,
                    NbEvaluation = l.NbEvaluation,
                    Img = l.Img,
                    Disponibilite = l.Disponibilite,
                    NomFournisseur = l.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = l.IdLogementCategorieNavigation.Nom,
                    NomPays = l.IdPaysNavigation.Nom,
                    PrixLogement = l.IdLogementPrixNavigation.Prix
                })
                .ToListAsync();

            return logements;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogementDTO>> GetLogement(int id)
        {
            var logement = await _context.Logements
                .Include(l => l.IdFournisseurNavigation)
                .Include(l => l.IdLogementCategorieNavigation)
                .Include(l => l.IdPaysNavigation)
                .Include(l => l.IdLogementPrixNavigation)
                .Where(l => l.IdLogement == id)
                .Select(l => new LogementDTO
                {
                    IdLogement = l.IdLogement,
                    Nom = l.Nom,
                    Details = l.Details,
                    Note = l.Note,
                    NbEvaluation = l.NbEvaluation,
                    Img = l.Img,
                    Disponibilite = l.Disponibilite,
                    NomFournisseur = l.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = l.IdLogementCategorieNavigation.Nom,
                    NomPays = l.IdPaysNavigation.Nom,
                    PrixLogement = l.IdLogementPrixNavigation.Prix
                })
                .FirstOrDefaultAsync();

            if (logement == null)
            {
                return NotFound();
            }

            return logement;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogement(int id, LogementDTO dto)
        {
            if (id != dto.IdLogement)
            {
                return BadRequest();
            }

            var logement = await _context.Logements.FindAsync(id);
            if (logement == null)
            {
                return NotFound();
            }

            logement.Nom = dto.Nom;
            logement.Details = dto.Details;
            logement.Note = dto.Note;
            logement.NbEvaluation = dto.NbEvaluation;
            logement.Img = dto.Img;
            logement.Disponibilite = dto.Disponibilite;
            // Update other fields if necessary

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

        private bool LogementExists(int id)
        {
            return _context.Logements.Any(e => e.IdLogement == id);
        }
    }
}

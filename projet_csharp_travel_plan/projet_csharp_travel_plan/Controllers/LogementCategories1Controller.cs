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
    public class LogementCategories1Controller : ControllerBase
    {
        private readonly TravelPlanContext _context;

        public LogementCategories1Controller(TravelPlanContext context)
        {
            _context = context;
        }

        // GET: api/LogementCategories1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogementCategorie>>> GetLogementCategories()
        {
            return await _context.LogementCategories.ToListAsync();
        }

        // GET: api/LogementCategories1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogementCategorie>> GetLogementCategorie(int id)
        {
            var logementCategorie = await _context.LogementCategories.FindAsync(id);

            if (logementCategorie == null)
            {
                return NotFound();
            }

            return logementCategorie;
        }

        // PUT: api/LogementCategories1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogementCategorie(int id, LogementCategorie logementCategorie)
        {
            if (id != logementCategorie.IdLogementCategorie)
            {
                return BadRequest();
            }

            _context.Entry(logementCategorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogementCategorieExists(id))
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

        // POST: api/LogementCategories1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogementCategorie>> PostLogementCategorie(LogementCategorie logementCategorie)
        {
            _context.LogementCategories.Add(logementCategorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogementCategorie", new { id = logementCategorie.IdLogementCategorie }, logementCategorie);
        }

        // DELETE: api/LogementCategories1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogementCategorie(int id)
        {
            var logementCategorie = await _context.LogementCategories.FindAsync(id);
            if (logementCategorie == null)
            {
                return NotFound();
            }

            _context.LogementCategories.Remove(logementCategorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogementCategorieExists(int id)
        {
            return _context.LogementCategories.Any(e => e.IdLogementCategorie == id);
        }
    }
}

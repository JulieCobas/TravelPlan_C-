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
    public class VoyagesController : ControllerBase
    {
        private readonly TravelPlanContext _context;

        public VoyagesController(TravelPlanContext context)
        {
            _context = context;
        }

        // GET: api/Voyages1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyages()
        {
            return await _context.Voyages.ToListAsync();
        }

        // GET: api/Voyages1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voyage>> GetVoyage(int id)
        {
            var voyage = await _context.Voyages.FindAsync(id);

            if (voyage == null)
            {
                return NotFound();
            }

            return voyage;
        }

        // PUT: api/Voyages1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoyage(int id, Voyage voyage)
        {
            if (id != voyage.IdVoyage)
            {
                return BadRequest();
            }

            _context.Entry(voyage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
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

        // POST: api/Voyages1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voyage>> PostVoyage(Voyage voyage)
        {
            _context.Voyages.Add(voyage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoyage", new { id = voyage.IdVoyage }, voyage);
        }

        // DELETE: api/Voyages1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyage(int id)
        {
            var voyage = await _context.Voyages.FindAsync(id);
            if (voyage == null)
            {
                return NotFound();
            }

            _context.Voyages.Remove(voyage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoyageExists(int id)
        {
            return _context.Voyages.Any(e => e.IdVoyage == id);
        }
    }
}

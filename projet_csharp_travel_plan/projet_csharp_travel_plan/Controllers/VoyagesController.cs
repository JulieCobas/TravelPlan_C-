using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class VoyagesController : ControllerBase
{
    private readonly TravelPlanContext _context;

    public VoyagesController(TravelPlanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VoyageDTO>>> GetVoyages()
    {
        var voyages = await _context.Voyages
            .Select(v => new VoyageDTO
            {
                IdVoyage = v.IdVoyage,
                DateDebut = v.DateDebut,
                DateFin = v.DateFin,
                // Ne pas inclure les pays ici
            })
            .ToListAsync();

        return voyages;
    }

    [HttpGet("GetPays")]
    public async Task<ActionResult<IEnumerable<PayDTO>>> GetPays()
    {
        var pays = await _context.Pays
            .Select(p => new PayDTO
            {
                IdPays = p.IdPays,
                Nom = p.Nom
            })
            .ToListAsync();

        return pays;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<VoyageDTO>> GetVoyage(int id)
    {
        var voyage = await _context.Voyages
            .Where(v => v.IdVoyage == id)
            .Select(v => new VoyageDTO
            {
                IdVoyage = v.IdVoyage,
                DateDebut = v.DateDebut,
                DateFin = v.DateFin,
                // Update the Pays property if it's directly related to Voyage
                Pays = v.IdPays.Select(p => p.Nom).ToList()
            })
            .FirstOrDefaultAsync();

        if (voyage == null)
        {
            return NotFound();
        }

        return voyage;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutVoyage(int id, VoyageDTO dto)
    {
        if (id != dto.IdVoyage)
        {
            return BadRequest();
        }

        var voyage = await _context.Voyages.FindAsync(id);
        if (voyage == null)
        {
            return NotFound();
        }

        voyage.DateDebut = dto.DateDebut;
        voyage.DateFin = dto.DateFin;
        // Update other fields if necessary

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

    [HttpPost]
    public async Task<ActionResult<Voyage>> PostVoyage(Voyage voyage)
    {
        _context.Voyages.Add(voyage);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetVoyage", new { id = voyage.IdVoyage }, voyage);
    }

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

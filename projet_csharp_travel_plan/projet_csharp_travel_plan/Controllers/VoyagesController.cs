using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;

[Route("api/[controller]")]
[ApiController]
public class VoyagesController : ControllerBase
{
    private readonly TravelPlanContext _context;

    public VoyagesController(TravelPlanContext context)
    {
        _context = context;
    }

    // GET: api/Voyages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VoyageDTO>>> GetVoyages()
    {
        var voyages = await _context.Voyages
            .Include(v => v.IdClientNavigation)
            .ToListAsync();

        var voyageDtos = voyages.Select(v => new VoyageDTO
        {
            IdVoyage = v.IdVoyage,
            DateDebut = v.DateDebut,
            DateFin = v.DateFin,
            PrixTotal = v.PrixTotal,
            StatutPaiement = v.StatutPaiement,
            Client = new ClientDTO
            {
                IdClient = v.IdClientNavigation.IdClient,
                Nom = v.IdClientNavigation.Nom,
                Prenom = v.IdClientNavigation.Prenom
            }
        }).ToList();

        return Ok(voyageDtos);
    }

    // GET: api/Voyages/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VoyageDTO>> GetVoyage(int id)
    {
        var voyage = await _context.Voyages
            .Include(v => v.IdClientNavigation)
            .Where(v => v.IdVoyage == id)
            .Select(v => new VoyageDTO
            {
                IdVoyage = v.IdVoyage,
                DateDebut = v.DateDebut,
                DateFin = v.DateFin,
                PrixTotal = v.PrixTotal,
                StatutPaiement = v.StatutPaiement,
                Client = new ClientDTO
                {
                    IdClient = v.IdClientNavigation.IdClient,
                    Nom = v.IdClientNavigation.Nom,
                    Prenom = v.IdClientNavigation.Prenom
                }
            })
            .FirstOrDefaultAsync();

        if (voyage == null)
        {
            return NotFound();
        }

        return voyage;
    }

    // PUT: api/Voyages/5
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

    // POST: api/Voyages
    [HttpPost]
    public async Task<ActionResult<Voyage>> PostVoyage(Voyage voyage)
    {
        _context.Voyages.Add(voyage);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetVoyage", new { id = voyage.IdVoyage }, voyage);
    }

    // DELETE: api/Voyages/5
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

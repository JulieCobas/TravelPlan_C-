using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

[Route("api/[controller]")]
[ApiController]
public class VoyagesController : ControllerBase
{
    private readonly TravelPlanNewDbContext _context;
    private readonly ILogger<VoyagesController> _logger;

    public VoyagesController(TravelPlanNewDbContext context, ILogger<VoyagesController> logger)
    {
        _context = context;
        _logger = logger;
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
                IdPays = v.IdPays.Select(p => p.IdPays).FirstOrDefault() // Select the first country ID
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
                Pays = v.IdPays.Select(p => new PayDTO
                {
                    IdPays = p.IdPays,
                    Nom = p.Nom
                }).ToList()
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

        var voyage = await _context.Voyages.Include(v => v.IdPays).FirstOrDefaultAsync(v => v.IdVoyage == id);
        if (voyage == null)
        {
            return NotFound();
        }

        voyage.DateDebut = dto.DateDebut;
        voyage.DateFin = dto.DateFin;

        // Update Pays relationship
        voyage.IdPays.Clear();
        foreach (var pay in dto.Pays)
        {
            var existingPay = await _context.Pays.FindAsync(pay.IdPays);
            if (existingPay != null)
            {
                voyage.IdPays.Add(existingPay);
            }
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

    [HttpPost]
    public async Task<ActionResult<VoyageDTO>> PostVoyage(VoyageDTO dto)
    {
        _logger.LogInformation("Starting PostVoyage method");

        var voyage = new Voyage
        {
            DateDebut = dto.DateDebut,
            DateFin = dto.DateFin,
            IdClient = dto.IdClient,
            PrixTotal = dto.PrixTotal,
            StatutPaiement = dto.StatutPaiement
        };

        _logger.LogInformation("Created voyage object with DateDebut: {DateDebut}, DateFin: {DateFin}", dto.DateDebut, dto.DateFin);

        foreach (var pay in dto.Pays)
        {
            var existingPay = await _context.Pays.FindAsync(pay.IdPays);
            if (existingPay != null)
            {
                voyage.IdPays.Add(existingPay);
                _logger.LogInformation("Added existing pay with Id: {IdPays} to voyage", pay.IdPays);
            }
        }

        _context.Voyages.Add(voyage);

        try
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saved changes to the database");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while saving voyage to the database");
            return StatusCode(500, "Internal server error");
        }

        dto.IdVoyage = voyage.IdVoyage;

        return CreatedAtAction(nameof(GetVoyage), new { id = voyage.IdVoyage }, dto);
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

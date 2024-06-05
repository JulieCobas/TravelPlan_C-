using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TransportsController : ControllerBase
{
    private readonly TravelPlanNewDbContext _context;

    public TransportsController(TravelPlanNewDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransportDTO>>> GetTransports()
    {
        var transports = await _context.Transports
            .Include(t => t.IdFournisseurNavigation)
            .Include(t => t.IdOptionTransportNavigation)
            .Include(t => t.IdCategorieTransportNavigation)
            .Include(t => t.IdPrixTransportNavigation)
            .Include(t => t.IdVehiculeLocNavigation)
            .ToListAsync();

        var transportDTOs = transports.Select(t => new TransportDTO
        {
            IdTransport = t.IdTransport,
            NomFournisseur = t.IdFournisseurNavigation.NomCompagnie,
            BagageMain = t.IdOptionTransportNavigation?.BagageMain,
            BagageEnSoute = t.IdOptionTransportNavigation?.BagageEnSoute,
            BagageLarge = t.IdOptionTransportNavigation?.BagageLarge,
            Speedyboarding = t.IdOptionTransportNavigation?.Speedyboarding,
            Prix = t.IdPrixTransportNavigation.Prix,
            CategorieTransportNom = t.IdCategorieTransportNavigation.Nom,
            VehiculeLocMarque = t.IdVehiculeLocNavigation?.Marque,
            VehiculeLocTypeVehicule = t.IdVehiculeLocNavigation?.TypeVehicule,
            VehiculeLocNbSiege = t.IdVehiculeLocNavigation?.NbSiege ?? 0 // Default to 0 if null
        }).ToList();

        return transportDTOs;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransportDTO>> GetTransport(int id)
    {
        var transport = await _context.Transports
            .Include(t => t.IdFournisseurNavigation)
            .Include(t => t.IdOptionTransportNavigation)
            .Include(t => t.IdCategorieTransportNavigation)  // Inclure la catégorie de transport
            .Include(t => t.IdPrixTransportNavigation)
            .Include(t => t.IdVehiculeLocNavigation)
            .Where(t => t.IdTransport == id)
            .FirstOrDefaultAsync();

        if (transport == null)
        {
            return NotFound();
        }

        var transportDto = new TransportDTO
        {
            IdTransport = transport.IdTransport,
            NomFournisseur = transport.IdFournisseurNavigation.NomCompagnie,
            Prix = transport.IdPrixTransportNavigation.Prix,
            VehiculeLocMarque = transport.IdVehiculeLocNavigation?.Marque,
            VehiculeLocTypeVehicule = transport.IdVehiculeLocNavigation?.TypeVehicule,
            VehiculeLocNbSiege = transport.IdVehiculeLocNavigation?.NbSiege ?? 0,
            BagageMain = transport.IdOptionTransportNavigation?.BagageMain,
            PrixBagageMain = transport.IdOptionTransportNavigation?.PrixBagagemain,
            BagageEnSoute = transport.IdOptionTransportNavigation?.BagageEnSoute,
            PrixBagageEnSoute = transport.IdOptionTransportNavigation?.PrixBagagesoute,
            BagageLarge = transport.IdOptionTransportNavigation?.BagageLarge,
            PrixBagageLarge = transport.IdOptionTransportNavigation?.PrixBagagelarge,
            Speedyboarding = transport.IdOptionTransportNavigation?.Speedyboarding,
            PrixSpeedyBoarding = transport.IdOptionTransportNavigation?.PrixSpeedyboarding,
            CategorieTransport = new TransportCategorieDTO
            {
                IdCategorieTransport = transport.IdCategorieTransportNavigation.IdCategorieTransport,
                Nom = transport.IdCategorieTransportNavigation.Nom
            }
        };

        return transportDto;
    }






    [HttpPut("{id}")]
    public async Task<IActionResult> PutTransport(int id, TransportDTO dto)
    {
        if (id != dto.IdTransport)
        {
            return BadRequest();
        }

        var transport = await _context.Transports.FindAsync(id);
        if (transport == null)
        {
            return NotFound();
        }

        transport.IdOptionTransportNavigation.BagageMain = dto.BagageMain;
        transport.IdOptionTransportNavigation.BagageEnSoute = dto.BagageEnSoute;
        transport.IdOptionTransportNavigation.BagageLarge = dto.BagageLarge;
        transport.IdOptionTransportNavigation.Speedyboarding = dto.Speedyboarding;
        // Update other fields if necessary

        _context.Entry(transport).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TransportExists(id))
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

    private bool TransportExists(int id)
    {
        return _context.Transports.Any(e => e.IdTransport == id);
    }
}

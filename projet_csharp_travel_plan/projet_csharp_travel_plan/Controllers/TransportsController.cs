using System.Collections.Generic;
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
    public class TransportsController : ControllerBase
    {
        private readonly TravelPlanContext _context;

        public TransportsController(TravelPlanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportDTO>>> GetTransports()
        {
            var transports = await _context.Transports
                .Include(t => t.IdCategorieTransportNavigation)
                .Include(t => t.IdFournisseurNavigation)
                .Include(t => t.IdOptionTransportNavigation)
                .Include(t => t.IdPrixTransportNavigation)
                .Include(t => t.IdVehiculeLocNavigation)
                .Select(t => new TransportDTO
                {
                    IdTransport = t.IdTransport,
                    LieuDepart = t.LieuDepart,
                    HeureDepart = t.HeureDepart,
                    HeureArrivee = t.HeureArrivee,
                    Classe = t.Classe,
                    NomCategorie = t.IdCategorieTransportNavigation.Nom,
                    NomFournisseur = t.IdFournisseurNavigation.NomCompagnie,
                    NomPays = t.IdPaysNavigation.Nom,
                    MarqueVehicule = t.IdVehiculeLocNavigation.Marque,
                    TypeVehicule = t.IdVehiculeLocNavigation.TypeVehicule,
                    NbSiegesVehicule = t.IdVehiculeLocNavigation.NbSiege,
                    PrixTransport = t.IdPrixTransportNavigation.Prix,
                    OptionBagageMain = t.IdOptionTransportNavigation.BagageMain,
                    OptionBagageEnSoute = t.IdOptionTransportNavigation.BagageEnSoute,
                    OptionBagageLarge = t.IdOptionTransportNavigation.BagageLarge,
                    OptionSpeedyboarding = t.IdOptionTransportNavigation.Speedyboarding,
                })
                .ToListAsync();

            return transports;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransportDTO>> GetTransport(int id)
        {
            var transport = await _context.Transports
                .Include(t => t.IdCategorieTransportNavigation)
                .Include(t => t.IdFournisseurNavigation)
                .Include(t => t.IdOptionTransportNavigation)
                .Include(t => t.IdPrixTransportNavigation)
                .Include(t => t.IdVehiculeLocNavigation)
                .Where(t => t.IdTransport == id)
                .Select(t => new TransportDTO
                {
                    IdTransport = t.IdTransport,
                    LieuDepart = t.LieuDepart,
                    HeureDepart = t.HeureDepart,
                    HeureArrivee = t.HeureArrivee,
                    Classe = t.Classe,
                    NomCategorie = t.IdCategorieTransportNavigation.Nom,
                    NomFournisseur = t.IdFournisseurNavigation.NomCompagnie,
                    NomPays = t.IdPaysNavigation.Nom,
                    MarqueVehicule = t.IdVehiculeLocNavigation.Marque,
                    TypeVehicule = t.IdVehiculeLocNavigation.TypeVehicule,
                    NbSiegesVehicule = t.IdVehiculeLocNavigation.NbSiege,
                    PrixTransport = t.IdPrixTransportNavigation.Prix,
                    OptionBagageMain = t.IdOptionTransportNavigation.BagageMain,
                    OptionBagageEnSoute = t.IdOptionTransportNavigation.BagageEnSoute,
                    OptionBagageLarge = t.IdOptionTransportNavigation.BagageLarge,
                    OptionSpeedyboarding = t.IdOptionTransportNavigation.Speedyboarding,
                })
                .FirstOrDefaultAsync();

            if (transport == null)
            {
                return NotFound();
            }

            return transport;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransport(int id, TransportDTO dto)
        {
            if (id != dto.IdTransport)
            {
                return BadRequest();
            }

            var transport = await _context.Transports
                .Include(t => t.IdOptionTransportNavigation)
                .FirstOrDefaultAsync(t => t.IdTransport == id);

            if (transport == null)
            {
                return NotFound();
            }

            // Ensure the IdOptionTransportNavigation is not null
            if (transport.IdOptionTransportNavigation == null)
            {
                transport.IdOptionTransportNavigation = new TransportOption();
            }

            // Update only the boolean properties
            transport.IdOptionTransportNavigation.BagageMain = dto.OptionBagageMain;
            transport.IdOptionTransportNavigation.BagageEnSoute = dto.OptionBagageEnSoute;
            transport.IdOptionTransportNavigation.BagageLarge = dto.OptionBagageLarge;
            transport.IdOptionTransportNavigation.Speedyboarding = dto.OptionSpeedyboarding;

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
}

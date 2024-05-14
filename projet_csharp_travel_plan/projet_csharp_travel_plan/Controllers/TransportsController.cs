using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/Transports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportDto>>> GetTransports()
        {
            var transports = await _context.Transports
                .Include(t => t.IdCategorieTransportNavigation)
                .Include(t => t.IdFournisseurNavigation)
                .Include(t => t.IdOptionTransportNavigation)
                .Include(t => t.IdPrixTransportNavigation)
                .Include(t => t.IdVehiculeLocNavigation)
                .ToListAsync();

            return transports.Select(t => new TransportDto
            {
                IdTransport = t.IdTransport,
                IdVehiculeLoc = t.IdVehiculeLoc,
                IdCategorieTransport = t.IdCategorieTransport,
                IdPrixTransport = t.IdPrixTransport,
                IdOptionTransport = t.IdOptionTransport,
                IdFournisseur = t.IdFournisseur,
                IdPays = t.IdPays,
                LieuDepart = t.LieuDepart,
                HeureDepart = t.HeureDepart,
                HeureArrivee = t.HeureArrivee,
                Classe = t.Classe,
                CategorieTransportNom = t.IdCategorieTransportNavigation?.Nom,
                FournisseurNomCompagnie = t.IdFournisseurNavigation?.NomCompagnie,
                VehiculeLocMarque = t.IdVehiculeLocNavigation?.Marque,
                VehiculeLocTypeVehicule = t.IdVehiculeLocNavigation?.TypeVehicule,
                VehiculeLocNbSiege = t.IdVehiculeLocNavigation?.NbSiege,
                OptionTransportNumeroSiege = t.IdOptionTransportNavigation?.NumeroSiege,
                OptionTransportBagageMain = t.IdOptionTransportNavigation?.BagageMain,
                OptionTransportBagageEnSoute = t.IdOptionTransportNavigation?.BagageEnSoute,
                OptionTransportBagageLarge = t.IdOptionTransportNavigation?.BagageLarge,
                OptionTransportSpeedyboarding = t.IdOptionTransportNavigation?.Speedyboarding,
                PrixTransportPrix = t.IdPrixTransportNavigation?.Prix
            }).ToList();
        }

        // GET: api/Transports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportDto>> GetTransport(int id)
        {
            var transport = await _context.Transports
                .Include(t => t.IdCategorieTransportNavigation)
                .Include(t => t.IdFournisseurNavigation)
                .Include(t => t.IdOptionTransportNavigation)
                .Include(t => t.IdPrixTransportNavigation)
                .Include(t => t.IdVehiculeLocNavigation)
                .FirstOrDefaultAsync(t => t.IdTransport == id);

            if (transport == null)
            {
                return NotFound();
            }

            var transportDto = new TransportDto
            {
                IdTransport = transport.IdTransport,
                IdVehiculeLoc = transport.IdVehiculeLoc,
                IdCategorieTransport = transport.IdCategorieTransport,
                IdPrixTransport = transport.IdPrixTransport,
                IdOptionTransport = transport.IdOptionTransport,
                IdFournisseur = transport.IdFournisseur,
                IdPays = transport.IdPays,
                LieuDepart = transport.LieuDepart,
                HeureDepart = transport.HeureDepart,
                HeureArrivee = transport.HeureArrivee,
                Classe = transport.Classe,
                CategorieTransportNom = transport.IdCategorieTransportNavigation?.Nom,
                FournisseurNomCompagnie = transport.IdFournisseurNavigation?.NomCompagnie,
                VehiculeLocMarque = transport.IdVehiculeLocNavigation?.Marque,
                VehiculeLocTypeVehicule = transport.IdVehiculeLocNavigation?.TypeVehicule,
                VehiculeLocNbSiege = transport.IdVehiculeLocNavigation?.NbSiege,
                OptionTransportNumeroSiege = transport.IdOptionTransportNavigation?.NumeroSiege,
                OptionTransportBagageMain = transport.IdOptionTransportNavigation?.BagageMain,
                OptionTransportBagageEnSoute = transport.IdOptionTransportNavigation?.BagageEnSoute,
                OptionTransportBagageLarge = transport.IdOptionTransportNavigation?.BagageLarge,
                OptionTransportSpeedyboarding = transport.IdOptionTransportNavigation?.Speedyboarding,
                PrixTransportPrix = transport.IdPrixTransportNavigation?.Prix
            };

            return transportDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransport(int id, TransportDto dto)
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
            transport.IdOptionTransportNavigation.BagageMain = dto.OptionTransportBagageMain;
            transport.IdOptionTransportNavigation.BagageEnSoute = dto.OptionTransportBagageEnSoute;
            transport.IdOptionTransportNavigation.BagageLarge = dto.OptionTransportBagageLarge;
            transport.IdOptionTransportNavigation.Speedyboarding = dto.OptionTransportSpeedyboarding;

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.Models;
using projet_csharp_travel_plan.DTO;

namespace projet_csharp_travel_plan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly TravelPlanNewDbContext _context;

        public PayController(TravelPlanNewDbContext context)
        {
            _context = context;
        }

        // GET: api/Pays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayDTO>>> GetPays()
        {
            return await _context.Pays
                .Include(p => p.Activites)
                .Include(p => p.Logements)
                .Include(p => p.Regions)
                .Include(p => p.Transports)
                .Include(p => p.IdVoyages)
                .Select(p => new PayDTO
                {
                    IdPays = p.IdPays,
                    Nom = p.Nom,
                    Activites = p.Activites.Select(a => a.Nom).ToList(),
                    Logements = p.Logements.Select(l => l.Nom).ToList(),
                    Regions = p.Regions.Select(r => r.Nom).ToList(),
                    Transports = p.Transports.Select(t => t.LieuDepart).ToList(), 
                    Voyages = p.IdVoyages.Select(v => v.DateDebut.ToString()).ToList() })
                .ToListAsync();
        }

        // GET: api/Pays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayDTO>> GetPay(short id)
        {
            var pay = await _context.Pays
                .Include(p => p.Activites)
                .Include(p => p.Logements)
                .Include(p => p.Regions)
                .Include(p => p.Transports)
                .Include(p => p.IdVoyages)
                .FirstOrDefaultAsync(p => p.IdPays == id);

            if (pay == null)
            {
                return NotFound();
            }

            var payDTO = new PayDTO
            {
                IdPays = pay.IdPays,
                Nom = pay.Nom,
                Activites = pay.Activites.Select(a => a.Nom).ToList(),
                Logements = pay.Logements.Select(l => l.Nom).ToList(),
                Regions = pay.Regions.Select(r => r.Nom).ToList(),
                Transports = pay.Transports.Select(t => t.LieuDepart).ToList(), 
                Voyages = pay.IdVoyages.Select(v => v.DateDebut.ToString()).ToList() 
            };

            return payDTO;
        }
    }
}

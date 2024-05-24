using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.Models;
using projet_csharp_travel_plan_frontend.DTO;

namespace projet_csharp_travel_plan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly TravelPlanContext _context;

            public PayController(TravelPlanContext context)
            {
                _context = context;
            }

            // GET: api/Pays
            [HttpGet]
            public async Task<ActionResult<IEnumerable<PayDTO>>> GetPays()
            {
                return await _context.Pays
                    .Select(p => new PayDTO
                    {
                        Nom = p.Nom,
                        // Autres propriétés de PayDTO si nécessaire
                    })
                    .ToListAsync();
            }
    }
}


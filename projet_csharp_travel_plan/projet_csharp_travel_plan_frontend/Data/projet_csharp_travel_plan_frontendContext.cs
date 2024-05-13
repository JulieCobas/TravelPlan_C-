using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan_frontend.Models;

namespace projet_csharp_travel_plan_frontend.Data
{
    public class projet_csharp_travel_plan_frontendContext : DbContext
    {
        public projet_csharp_travel_plan_frontendContext (DbContextOptions<projet_csharp_travel_plan_frontendContext> options)
            : base(options)
        {
        }

        public DbSet<projet_csharp_travel_plan_frontend.Models.Logement> Logement { get; set; } = default!;
    }
}

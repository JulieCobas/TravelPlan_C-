using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan_frontend.Models;

namespace projet_csharp_travel_plan_frontend.Areas.Identity.Data;

public class TravelPlanContext : IdentityDbContext<AppUser>
{
    public TravelPlanContext(DbContextOptions<TravelPlanContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

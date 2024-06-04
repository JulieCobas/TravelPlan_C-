using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace projet_csharp_travel_plan_frontend.Models;

public partial class TravelPlanNewDbContext : IdentityDbContext
{
    public TravelPlanNewDbContext()
    {
    }

    public TravelPlanNewDbContext(DbContextOptions<TravelPlanNewDbContext> options)
        : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

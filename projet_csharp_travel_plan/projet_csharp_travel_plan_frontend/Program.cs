<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
=======
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using projet_csharp_travel_plan_frontend.Areas.Identity.Data;
using projet_csharp_travel_plan_frontend.Models;
>>>>>>> identity_branch


namespace projet_csharp_travel_plan_frontend
{ 
    public class Program
    {
        public static void Main(String[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration settings and services
            var connectionString = builder.Configuration.GetConnectionString("TravelPlanConnection") ?? throw new InvalidOperationException("Connection string 'TravelPlanContextConnection' not found.");
            builder.Services.AddDbContext<TravelPlanContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TravelPlanContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

    }
}
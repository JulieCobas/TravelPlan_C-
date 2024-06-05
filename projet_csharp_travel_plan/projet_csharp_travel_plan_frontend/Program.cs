using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using projet_csharp_travel_plan_frontend.Models;
using projet_csharp_travel_plan_frontend.Authentication;
using Microsoft.Extensions.Logging;

namespace projet_csharp_travel_plan_frontend
{
    public class Program
    {
        public static void Main(String[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration settings and services
            var connectionString = builder.Configuration.GetConnectionString("TravelPlanConnection") ?? throw new InvalidOperationException("Connection string 'TravelPlanContextConnection' not found.");
            builder.Services.AddDbContext<TravelPlanNewDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TravelPlanNewDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Add HttpClient globally with ApiKeyHandler
            builder.Services.AddTransient<ApiKeyHandler>();
            builder.Services.AddHttpClient("default")
                .AddHttpMessageHandler<ApiKeyHandler>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (Exception ex)
                {
                    context.Response.Redirect("/Home/Error");
                }
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

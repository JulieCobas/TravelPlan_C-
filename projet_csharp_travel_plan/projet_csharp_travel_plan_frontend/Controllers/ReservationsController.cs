using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO; // Utilisation du DTO local au frontend

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Reservations/";
        private const string PAYS_API_URL = "https://localhost:7287/api/Pay/";

        public ReservationsController(HttpClient client)
        {
            _client = client;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservations = JsonConvert.DeserializeObject<List<ReservationDTO>>(json); // Utilisation du DTO local au frontend
                return View(reservations);
            }
            return View("Error");
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            var response = await _client.GetAsync(PAYS_API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<PayDTO>>(json);

                ViewBag.Countries = new SelectList(countries, "Nom", "Nom");
                ViewBag.Options = new SelectList(new List<string> { "Logement", "Activité", "Transport" });

                return View();
            }

            return View("Error");
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string selectedCountry, string selectedOption)
        {
            if (string.IsNullOrEmpty(selectedCountry) || string.IsNullOrEmpty(selectedOption))
            {
                return View();
            }

            switch (selectedOption)
            {
                case "Logement":
                    return RedirectToAction("Index", "Logements", new { country = selectedCountry });
                case "Activité":
                    return RedirectToAction("Index", "Activites", new { country = selectedCountry });
                case "Transport":
                    return RedirectToAction("Index", "Transports", new { country = selectedCountry });
                default:
                    return View();
            }
        }
    }
}

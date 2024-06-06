using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class LogementsController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Logements/";
        private const string RESERVATION_API_URL = "https://localhost:7287/api/Reservations/";

        public LogementsController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("default");
        }

        // GET: Logements
        public async Task<IActionResult> Index(string country, int voyageId)
        {
            var response = await _client.GetAsync($"{API_URL}?country={country}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var logements = JsonConvert.DeserializeObject<List<LogementDTO>>(json);
                logements = logements.Where(l => l.NomPays == country).ToList();
                ViewData["SelectedCountry"] = country;
                ViewData["VoyageId"] = voyageId;
                return View(logements);
            }

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Error", "Home", new { message = errorMessage });
        }

        // GET: Logements/Details/5
        public async Task<IActionResult> Details(int id, string country, int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var logement = JsonConvert.DeserializeObject<LogementDTO>(json);
                    ViewData["SelectedCountry"] = country;
                    ViewData["VoyageId"] = voyageId == 0 ? 1 : voyageId; // Default to 1 if voyageId is 0
                    return View(logement);
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (Exception ex)
            {
                // Handle exception
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReservation(short IdLogement, short VoyageId, DateTime DateDebut, DateTime DateFin)
        {
            if (VoyageId == 0) VoyageId = 1; // Default to 1 if VoyageId is 0

            var reservation = new ReservationDTO
            {
                IdLogement = IdLogement,
                IdVoyage = VoyageId,
                DateHeureDebut = DateDebut,
                DateHeureFin = DateFin,
                Disponibilite = true
            };

            var response = await _client.PostAsJsonAsync(RESERVATION_API_URL, reservation);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Reservations");
            }

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Error", "Home", new { message = errorMessage });
        }
    }
}

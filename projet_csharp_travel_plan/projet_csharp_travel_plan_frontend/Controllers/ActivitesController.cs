using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    [Authorize]
    public class ActivitesController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Activites/";
        private const string RESERVATION_API_URL = "https://localhost:7287/api/Reservations/";

        public ActivitesController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("default");
        }

        // GET: Activites
        public async Task<IActionResult> Index(string country, int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}?country={country}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var activites = JsonConvert.DeserializeObject<List<ActiviteDTO>>(json);
                    activites = activites.Where(a => a.NomPays == country).ToList();
                    ViewData["SelectedCountry"] = country;
                    ViewData["VoyageId"] = voyageId;
                    return View(activites);
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        // GET: Activites/Details/5
        public async Task<IActionResult> Details(int id, string country, int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var activite = JsonConvert.DeserializeObject<ActiviteDTO>(json);
                    ViewData["SelectedCountry"] = country;
                    ViewData["VoyageId"] = voyageId == 0 ? 1 : voyageId; // Default to 1 if voyageId is 0
                    return View(activite);
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
        public async Task<IActionResult> ConfirmActiviteSelection(short IdActivite, short VoyageId, DateTime DateDebut, DateTime DateFin, TimeSpan HeureDebut, bool GuideAudio, bool VisiteGuidee)
        {
            if (VoyageId == 0) VoyageId = 1; // Default to 1 if VoyageId is 0

            try
            {
                var reservation = new ReservationDTO
                {
                    IdActivite = IdActivite,
                    IdVoyage = VoyageId,
                    DateHeureDebut = DateDebut.Add(HeureDebut),
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
            catch (HttpRequestException ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }
    }
}

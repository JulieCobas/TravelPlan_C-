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
using Microsoft.Extensions.Logging;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    [Authorize]
    public class LogementsController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<LogementsController> _logger;
        private const string API_URL = "https://localhost:7287/api/Logements/";
        private const string RESERVATION_API_URL = "https://localhost:7287/api/Reservations/";

        public LogementsController(IHttpClientFactory httpClientFactory, ILogger<LogementsController> logger)
        {
            _client = httpClientFactory.CreateClient("default");
            _logger = logger;
        }

        public async Task<IActionResult> Index(int voyageId)
        {
            try
            {
                var response = await _client.GetAsync(API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var logements = JsonConvert.DeserializeObject<List<LogementDTO>>(json);
                    ViewData["VoyageId"] = voyageId;
                    return View(logements);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Logements action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        public async Task<IActionResult> Details(int id, int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var logement = JsonConvert.DeserializeObject<LogementDTO>(json);
                    ViewData["VoyageId"] = voyageId;
                    return View(logement);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Logement Details action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReservation(short IdLogement, short VoyageId, DateTime DateDebut, DateTime DateFin)
        {
            var reservation = new ReservationDTO
            {
                IdLogement = IdLogement,
                IdVoyage = VoyageId,
                DateHeureDebut = DateDebut,
                DateHeureFin = DateFin,
                Disponibilite = true
            };

            try
            {
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
                _logger.LogError("Error in POST ConfirmReservation action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }
    }
}

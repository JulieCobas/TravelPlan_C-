using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<ReservationsController> _logger;
        private const string API_URL = "https://localhost:7287/api/Reservations/";
        private const string PAYS_API_URL = "https://localhost:7287/api/Pay/";
        private const string VOYAGE_API_URL = "https://localhost:7287/api/Voyages/";

        public ReservationsController(IHttpClientFactory httpClientFactory, ILogger<ReservationsController> logger)
        {
            _client = httpClientFactory.CreateClient("default");
            _logger = logger;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _client.GetAsync(API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var reservations = JsonConvert.DeserializeObject<List<ReservationDTO>>(json);
                    return View(reservations);
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Reservations action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        // GET: Reservations
        public async Task<IActionResult> Timeline()
        {
            try
            {
                var response = await _client.GetAsync(API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var reservations = JsonConvert.DeserializeObject<List<ReservationDTO>>(json);
                    return View(reservations);
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Reservations action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                var response = await _client.GetAsync(PAYS_API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var countries = JsonConvert.DeserializeObject<List<PayDTO>>(json);

                    ViewBag.Countries = new SelectList(countries, "Nom", "Nom");

                    // Get the selected country for the voyage
                    var voyageResponse = await _client.GetAsync($"{VOYAGE_API_URL}{id}");
                    if (voyageResponse.IsSuccessStatusCode)
                    {
                        var voyageJson = await voyageResponse.Content.ReadAsStringAsync();
                        var voyage = JsonConvert.DeserializeObject<VoyageDTO>(voyageJson);
                        ViewBag.SelectedCountry = countries.FirstOrDefault(c => c.IdPays == voyage.IdPays)?.Nom;
                    }

                    ViewBag.Options = new SelectList(new List<string> { "Logement", "Activité", "Transport" });
                    ViewBag.VoyageId = id;

                    return View();
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Create action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int idVoyage, string selectedCountry, string selectedOption)
        {
            if (string.IsNullOrEmpty(selectedCountry) || string.IsNullOrEmpty(selectedOption))
            {
                // Load countries and options again if validation fails
                ViewBag.Countries = new SelectList(new List<PayDTO>(), "Nom", "Nom");
                ViewBag.Options = new SelectList(new List<string> { "Logement", "Activité", "Transport" });
                return View();
            }

            TempData["VoyageId"] = idVoyage;
            TempData["Country"] = selectedCountry;

            switch (selectedOption)
            {
                case "Logement":
                    return RedirectToAction("Index", "Logements", new { country = selectedCountry, voyageId = idVoyage });
                case "Activité":
                    return RedirectToAction("Index", "Activites", new { country = selectedCountry, voyageId = idVoyage });
                case "Transport":
                    return RedirectToAction("Index", "Transports", new { voyageId = idVoyage });
                default:
                    return View();
            }
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _client.GetAsync(API_URL + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var reservation = JsonConvert.DeserializeObject<ReservationDTO>(json);
                    return View(reservation);
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Details action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        // GET reservations/byvoyages/{idvoyage}
        public async Task<IActionResult> ByVoyage(int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}byvoyage/{voyageId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var reservations = JsonConvert.DeserializeObject<List<ReservationDTO>>(json);
                    return View("Timeline", reservations); // Utilise la vue "Timeline" pour afficher les réservations filtrées
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Reservations by voyage action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }


    }
}

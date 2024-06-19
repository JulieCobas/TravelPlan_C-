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

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _client.GetAsync(API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var reservations = JsonConvert.DeserializeObject<List<ReservationPaysModelDTO>>(json);

                    // Récupérer la liste des pays
                    var countryResponse = await _client.GetAsync(PAYS_API_URL);
                    if (countryResponse.IsSuccessStatusCode)
                    {
                        var countryJson = await countryResponse.Content.ReadAsStringAsync();
                        var countries = JsonConvert.DeserializeObject<List<PayDTO>>(countryJson);

                        foreach (var reservation in reservations)
                        {
                            if (reservation.Voyage != null)
                            {
                                var country = countries.FirstOrDefault(c => c.IdPays == reservation.Voyage.IdVoyage); // Correspondance par ID
                                if (country != null)
                                {
                                    // Assigner le nom du pays au nouveau DTO
                                    reservation.Voyage.NomPays = country.Nom;
                                }
                            }
                        }

                        return View(reservations);
                    }
                    else
                    {
                        var errorCountryMessage = await countryResponse.Content.ReadAsStringAsync();
                        _logger.LogError("Error fetching countries: {Message}", errorCountryMessage);
                        return RedirectToAction("Error", "Home", new { message = errorCountryMessage });
                    }
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error fetching reservations: {Message}", errorMessage);
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var clientId = await GetClientIdAsync(userId);

                ViewBag.Voyages = new SelectList(await GetVoyagesByClient(clientId), "IdVoyage", "IdVoyage");
                ViewBag.Countries = new SelectList(await GetCountries(), "IdPays", "Nom");
                ViewBag.Options = new SelectList(new List<string> { "Logement", "Activité", "Transport" });
                return View(new ReservationCreateViewModel());
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
        public async Task<IActionResult> Create(ReservationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var countries = await GetCountries();
                var selectedCountryName = countries.FirstOrDefault(c => c.IdPays == model.SelectedCountry)?.Nom;

                TempData["VoyageId"] = model.IdVoyage;
                TempData["SelectedCountry"] = selectedCountryName; // Stocker le nom du pays

                switch (model.SelectedOption)
                {
                    case "Logement":
                        return RedirectToAction("Index", "Logements", new { voyageId = model.IdVoyage, country = selectedCountryName });
                    case "Activité":
                        return RedirectToAction("Index", "Activites", new { voyageId = model.IdVoyage, country = selectedCountryName });
                    case "Transport":
                        return RedirectToAction("Index", "Transports", new { voyageId = model.IdVoyage, country = selectedCountryName });
                    default:
                        return View(model);
                }
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var clientId = await GetClientIdAsync(userId);
            ViewBag.Voyages = new SelectList(await GetVoyagesByClient(clientId), "IdVoyage", "IdVoyage");
            ViewBag.Countries = new SelectList(await GetCountries(), "IdPays", "Nom");
            ViewBag.Options = new SelectList(new List<string> { "Logement", "Activité", "Transport" });
            return View(model);
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

        private async Task<short> GetClientIdAsync(string userId)
        {
            var response = await _client.GetAsync($"https://localhost:7287/api/Voyages/GetByUserId?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var client = JsonConvert.DeserializeObject<ClientDTO>(json);
                return client.IdClient;
            }
            return 0; // Default value if client not found
        }

        private async Task<List<VoyageDTO>> GetVoyagesByClient(short clientId)
        {
            var response = await _client.GetAsync($"https://localhost:7287/api/Voyages/VoyagesByClient/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<VoyageDTO>>(json);
            }
            return new List<VoyageDTO>();
        }

        private async Task<List<PayDTO>> GetCountries()
        {
            var response = await _client.GetAsync(PAYS_API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PayDTO>>(json);
            }
            return new List<PayDTO>();
        }
    }
}
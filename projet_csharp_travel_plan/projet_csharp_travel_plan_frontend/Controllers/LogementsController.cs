using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Diagnostics;

[Authorize]
public class LogementsController : Controller
{
    private readonly HttpClient _client;
    private readonly ILogger<LogementsController> _logger;
    private const string API_URL = "https://localhost:7287/api/Logements/";

    public LogementsController(IHttpClientFactory httpClientFactory, ILogger<LogementsController> logger)
    {
        _client = httpClientFactory.CreateClient("default");
        _logger = logger;
    }

    public async Task<IActionResult> Index(string country, int voyageId)
    {
        try
        {
            var response = await _client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var logements = JsonConvert.DeserializeObject<List<LogementDTO>>(json);

                // Filtrer les logements par pays
                logements = logements.Where(l => l.NomPays.Equals(country, StringComparison.OrdinalIgnoreCase)).ToList();

                ViewData["SelectedCountry"] = country;
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
            var response = await _client.PostAsJsonAsync("https://localhost:7287/api/Reservations", reservation);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Reservations");
            }

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

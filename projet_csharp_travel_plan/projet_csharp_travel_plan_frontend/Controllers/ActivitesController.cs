using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Diagnostics;

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
    public async Task<IActionResult> Index(string country)
    {
        var response = await _client.GetAsync($"{API_URL}?country={country}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var activites = JsonConvert.DeserializeObject<List<ActiviteDTO>>(json);
            activites = activites.Where(a => a.NomPays == country).ToList();
            ViewData["SelectedCountry"] = country;
            return View(activites);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
        return View("Error", errorModel);
    }

    // GET: Activites/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var response = await _client.GetAsync($"{API_URL}{id}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var activite = JsonConvert.DeserializeObject<ActiviteDTO>(json);
            return View(activite);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
        return View("Error", errorModel);
    }

    // POST: Activites/ConfirmActiviteSelection
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmActiviteSelection(short IdActivite, DateTime DateDebut, DateTime DateFin, TimeSpan HeureDebut, bool GuideAudio, bool VisiteGuidee)
    {
        var reservation = new ReservationDTO
        {
            IdActivite = IdActivite,
            DateHeureDebut = DateDebut.Add(HeureDebut),
            DateHeureFin = DateFin,
            Disponibilite = true
        };

        var response = await _client.PostAsJsonAsync("https://localhost:7287/api/Reservations", reservation);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Reservations");
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
        return View("Error", errorModel);
    }

}

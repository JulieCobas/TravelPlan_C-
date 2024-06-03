using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Diagnostics;

public class LogementsController : Controller
{
    private readonly HttpClient _client;
    private const string API_URL = "https://localhost:7287/api/Logements/";

    public LogementsController(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("default");
    }

    // GET: Logements
    public async Task<IActionResult> Index(string country)
    {
        var response = await _client.GetAsync($"{API_URL}?country={country}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var logements = JsonConvert.DeserializeObject<List<LogementDTO>>(json);
            logements = logements.Where(l => l.NomPays == country).ToList();
            ViewData["SelectedCountry"] = country;
            return View(logements);
        }

        // Handle error
        var errorMessage = await response.Content.ReadAsStringAsync();
        var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
        return View("Error", errorModel);
    }

    // GET: Logements/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var response = await _client.GetAsync($"{API_URL}{id}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var logement = JsonConvert.DeserializeObject<LogementDTO>(json);
            return View(logement);
        }

        // Handle error
        var errorMessage = await response.Content.ReadAsStringAsync();
        var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
        return View("Error", errorModel);
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO; // Utilisation du DTO local au frontend

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class LogementsController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Logements/";

        public LogementsController(HttpClient client)
        {
            _client = client;
        }

        // GET: Logements
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync($"{API_URL}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var logements = JsonConvert.DeserializeObject<List<LogementDTO>>(json); // Utilisation du DTO local au frontend
                return View(logements);
            }
            return View("Error");
        }


        // GET: Logements/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{API_URL}{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var logement = JsonConvert.DeserializeObject<LogementDTO>(json); // Utilisation du DTO local au frontend
                return View(logement);
            }
            return View("Error");
        }
    }
}

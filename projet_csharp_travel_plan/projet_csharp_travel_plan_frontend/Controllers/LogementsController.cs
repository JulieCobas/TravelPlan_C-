using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.Models;

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
            var response = await _client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var logements = JsonConvert.DeserializeObject<List<Logement>>(json);
                return View(logements);
            }
            return View("Error");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{API_URL}{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var logement = JsonConvert.DeserializeObject<Logement>(json);
                return View(logement);
            }
            return View("Error");
        }
    }
}

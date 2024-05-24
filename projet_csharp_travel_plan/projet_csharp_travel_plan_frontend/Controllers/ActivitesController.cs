using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class ActivitesController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Activites/";

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
                return View(activites);
            }

            // Handle error
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

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            return View("Error", errorModel);
        }
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class ActivitesController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Activites/";

        public ActivitesController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var activites = JsonConvert.DeserializeObject<List<ActiviteDTO>>(json);
                return View(activites);
            }
            return View("Error");
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
            return View("Error");
        }
    }
}

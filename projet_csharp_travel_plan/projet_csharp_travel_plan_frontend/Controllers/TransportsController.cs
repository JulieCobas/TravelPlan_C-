using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.Models;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class TransportsController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Transports/";

        public TransportsController(HttpClient client)
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
                var transports = JsonConvert.DeserializeObject<List<Transport>>(json);
                return View(transports);
            }
            return View("Error");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{API_URL}{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var transport = JsonConvert.DeserializeObject<Transport>(json);
                return View(transport);
            }
            return View("Error");
        }
    }
}

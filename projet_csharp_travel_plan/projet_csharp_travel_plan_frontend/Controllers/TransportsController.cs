using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class TransportsController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Transports/";

        public TransportsController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("default");
        }

        // GET: Transports
        public async Task<IActionResult> Index(string category = null)
        {
            var response = await _client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var transportDtos = JsonConvert.DeserializeObject<List<TransportDTO>>(json);

                if (!string.IsNullOrEmpty(category))
                {
                    transportDtos = transportDtos.Where(t => t.CategorieTransportNom == category).ToList();
                }

                ViewData["SelectedCategory"] = category;
                return View(transportDtos);
            }

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            return View("Error", errorModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{API_URL}{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var transportDto = JsonConvert.DeserializeObject<TransportDTO>(json);
                return View(transportDto);
            }

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            return View("Error", errorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmTransportSelection(int IdTransport, bool BagageMain, bool BagageEnSoute, bool BagageLarge, bool Speedyboarding)
        {
            var response = await _client.GetAsync($"{API_URL}{IdTransport}");
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
                return View("Error", errorModel);
            }

            var json = await response.Content.ReadAsStringAsync();
            var transportDto = JsonConvert.DeserializeObject<TransportDTO>(json);

            transportDto.BagageMain = BagageMain;
            transportDto.BagageEnSoute = BagageEnSoute;
            transportDto.BagageLarge = BagageLarge;
            transportDto.Speedyboarding = Speedyboarding;

            var updateResponse = await _client.PutAsJsonAsync($"{API_URL}{transportDto.IdTransport}", transportDto);
            if (!updateResponse.IsSuccessStatusCode)
            {
                var errorMessage = await updateResponse.Content.ReadAsStringAsync();
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
                return View("Error", errorModel);
            }

            return RedirectToAction("Reservations");
        }

        // New action to proceed to lodging reservation without selecting transport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProceedToLodging()
        {
            // Here you can add any logic you need before redirecting
            return RedirectToAction("Index", "Logements");
        }
    }
}

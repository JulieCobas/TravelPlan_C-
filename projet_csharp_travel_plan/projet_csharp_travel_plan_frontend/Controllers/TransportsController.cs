using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        // GET: Transports
        public async Task<IActionResult> Index(string category = null)
        {
            var response = await _client.GetAsync(API_URL);
            List<TransportDto> transportDtos = new List<TransportDto>();
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                transportDtos = JsonConvert.DeserializeObject<List<TransportDto>>(json);

                if (!string.IsNullOrEmpty(category))
                {
                    transportDtos = transportDtos.Where(t => t.CategorieTransportNom == category).ToList();
                }
            }
            else
            {
                return View("Error");
            }

            ViewData["SelectedCategory"] = category;
            return View(transportDtos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{API_URL}{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var transportDto = JsonConvert.DeserializeObject<TransportDto>(json);
                return View(transportDto);
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmSelection(Dictionary<string, TransportOptionsViewModel> options)
        {
            foreach (var option in options)
            {
                int idTransport = int.Parse(option.Key);
                var response = await _client.GetAsync($"{API_URL}{idTransport}");
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }

                var json = await response.Content.ReadAsStringAsync();
                var transportDto = JsonConvert.DeserializeObject<TransportDto>(json);

                transportDto.OptionTransportBagageMain = option.Value.BagageMain;
                transportDto.OptionTransportBagageEnSoute = option.Value.BagageEnSoute;
                transportDto.OptionTransportBagageLarge = option.Value.BagageLarge;
                transportDto.OptionTransportSpeedyboarding = option.Value.Speedyboarding;

                var updateResponse = await _client.PutAsJsonAsync($"{API_URL}{transportDto.IdTransport}", transportDto);
                if (!updateResponse.IsSuccessStatusCode)
                {
                    return View("Error");
                }
            }

            return RedirectToAction("Index", "Logements");
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

    public class UpdateBooleanRequest
    {
        public int Id { get; set; }
        public string Field { get; set; }
        public bool Value { get; set; }
    }

    public class TransportOptionsViewModel
    {
        public bool BagageMain { get; set; }
        public bool BagageEnSoute { get; set; }
        public bool BagageLarge { get; set; }
        public bool Speedyboarding { get; set; }
        public int IdTransport { get; set; }  // Ensure we have the IdTransport for each option set
    }
}
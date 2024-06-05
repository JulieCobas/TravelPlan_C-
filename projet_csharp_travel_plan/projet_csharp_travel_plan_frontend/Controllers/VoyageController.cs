using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class VoyageController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<VoyageController> _logger;
        private const string API_URL = "https://localhost:7287/api/Voyages/";
        private const string PAYS_API_URL = "https://localhost:7287/api/Pay/";

        public VoyageController(IHttpClientFactory httpClientFactory, ILogger<VoyageController> logger)
        {
            _client = httpClientFactory.CreateClient("default");
            _logger = logger;
        }

        // GET: Voyage
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var voyageDtos = JsonConvert.DeserializeObject<List<VoyageDTO>>(json);

                // Fetch the list of countries
                var paysResponse = await _client.GetAsync($"{API_URL}GetPays");
                if (paysResponse.IsSuccessStatusCode)
                {
                    var paysJson = await paysResponse.Content.ReadAsStringAsync();
                    var paysDtos = JsonConvert.DeserializeObject<List<PayDTO>>(paysJson);
                    ViewBag.Pays = paysDtos;
                }

                return View(voyageDtos);
            }

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Error", "Home", new { message = errorMessage });
        }

        // GET: Voyage/Create
        public async Task<IActionResult> Create()
        {
            var paysResponse = await _client.GetAsync(PAYS_API_URL);
            if (paysResponse.IsSuccessStatusCode)
            {
                var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                var pays = JsonConvert.DeserializeObject<List<PayDTO>>(jsonPays);
                var voyageDto = new VoyageDTO
                {
                    DateDebut = DateTime.Now,
                    DateFin = DateTime.Now.AddDays(7),
                    Pays = pays
                };

                ViewBag.Pays = pays;
                return View(voyageDto);
            }

            // Handle error
            var errorMessage = await paysResponse.Content.ReadAsStringAsync();
            return RedirectToAction("Error", "Home", new { message = errorMessage });
        }

        // POST: Voyage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoyageDTO voyage)
        {
            _logger.LogInformation("Starting Create action in VoyageController");

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(voyage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Sending POST request to API with payload: {Json}", json);

                var response = await _client.PostAsync(API_URL, content);
                if (response.IsSuccessStatusCode)
                {
                    var createdVoyage = JsonConvert.DeserializeObject<VoyageDTO>(await response.Content.ReadAsStringAsync());
                    _logger.LogInformation("Voyage created successfully with ID: {IdVoyage}", createdVoyage.IdVoyage);

                    // Store the created Voyage ID for future use
                    TempData["VoyageId"] = createdVoyage.IdVoyage;
                    return RedirectToAction("Index", "Reservations", new { id = createdVoyage.IdVoyage });
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error occurred while creating voyage: {ErrorMessage}", errorMessage);

                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }

            // If validation fails, reload the list of countries
            _logger.LogWarning("Model state is invalid, reloading countries list");
            var paysResponse = await _client.GetAsync(PAYS_API_URL);
            if (paysResponse.IsSuccessStatusCode)
            {
                var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                var pays = JsonConvert.DeserializeObject<List<PayDTO>>(jsonPays);
                voyage.Pays = pays;
            }

            return View(voyage);
        }
    }
}

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
    [Route("[controller]")]
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

        // GET: Voyage/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("GET Create action called.");
            var paysResponse = await _client.GetAsync(PAYS_API_URL);
            if (paysResponse.IsSuccessStatusCode)
            {
                var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                var pays = JsonConvert.DeserializeObject<List<PayDTO>>(jsonPays);
                var voyageDto = new VoyageDTO
                {
                    DateDebut = DateTime.Now,
                    DateFin = DateTime.Now.AddDays(7),
                    IdPays = pays.FirstOrDefault()?.IdPays ?? 0 // Utilise le premier pays de la liste
                };

                ViewBag.Pays = pays;
                _logger.LogInformation("GET Create action completed successfully.");
                return View(voyageDto);
            }

            // Handle error
            var errorMessage = await paysResponse.Content.ReadAsStringAsync();
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            _logger.LogError("Error in GET Create action: {ErrorMessage}", errorMessage);
            return View("Error", errorModel);
        }

        // POST: Voyage/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(VoyageDTO voyage)
        {
            _logger.LogInformation("POST Create action called.");

            voyage.IdClient = 1; // Set default client ID
            voyage.PrixTotal = 0; // Set default price total
            voyage.StatutPaiement = false; // Set default payment status

            if (ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is valid. Sending POST request to API.");
                var json = JsonConvert.SerializeObject(voyage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

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

                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
                return View("Error", errorModel);
            }

            _logger.LogWarning("ModelState is invalid. Reloading countries list.");
            // If validation fails, reload the list of countries
            var paysResponse = await _client.GetAsync(PAYS_API_URL);
            if (paysResponse.IsSuccessStatusCode)
            {
                var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                var pays = JsonConvert.DeserializeObject<List<PayDTO>>(jsonPays);
                ViewBag.Pays = pays;
            }

            return View(voyage);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class VoyageController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<VoyageController> _logger;
        private const string API_URL = "https://localhost:7287/api/Voyages/";
        private const string PAYS_API_URL = "https://localhost:7287/api/Pay/";
        private const string CLIENT_API_URL = "https://localhost:7287/api/Clients/";

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
            try
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
                        IdPays = pays.FirstOrDefault()?.IdPays ?? 0 // Utilise le premier pays de la liste
                    };

                    ViewBag.Pays = pays;
                    _logger.LogInformation("GET Create action completed successfully.");
                    return View(voyageDto);
                }

                // Handle error
                var errorMessage = await paysResponse.Content.ReadAsStringAsync();
                return CreateErrorView("Une erreur est survenue lors de la récupération des pays.", errorMessage);
            }
            catch (HttpRequestException ex)
            {
                return CreateErrorView("Une erreur est survenue lors de la récupération des pays.", ex.Message);
            }
        }

        // POST: Voyage/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(VoyageDTO voyage)
        {
            _logger.LogInformation("POST Create action called.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return CreateErrorView("L'utilisateur n'est pas authentifié.");
            }

            // Retrieve client based on user ID
            try
            {
                var clientResponse = await _client.GetAsync($"{CLIENT_API_URL}{userId}");
                if (clientResponse.IsSuccessStatusCode)
                {
                    var jsonClient = await clientResponse.Content.ReadAsStringAsync();
                    var client = JsonConvert.DeserializeObject<ClientDTO>(jsonClient);
                    if (client == null)
                    {
                        return CreateErrorView("Client non trouvé.");
                    }
                    voyage.IdClient = client.IdClient;
                }
                else
                {
                    var errorMessage = await clientResponse.Content.ReadAsStringAsync();
                    return CreateErrorView("Erreur lors de la récupération du client.", errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                return CreateErrorView("Erreur lors de la récupération du client.", ex.Message);
            }

            voyage.PrixTotal = 0; // Set default price total
            voyage.StatutPaiement = false; // Set default payment status

            if (ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is valid. Sending POST request to API.");
                var json = JsonConvert.SerializeObject(voyage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
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
                    return CreateErrorView("Erreur lors de la création du voyage.", errorMessage);
                }
                catch (HttpRequestException ex)
                {
                    return CreateErrorView("Erreur lors de la création du voyage.", ex.Message);
                }
            }

            _logger.LogWarning("ModelState is invalid. Reloading countries list.");
            // If validation fails, reload the list of countries
            try
            {
                var paysResponse = await _client.GetAsync(PAYS_API_URL);
                if (paysResponse.IsSuccessStatusCode)
                {
                    var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                    var pays = JsonConvert.DeserializeObject<List<PayDTO>>(jsonPays);
                    ViewBag.Pays = pays;
                }
                else
                {
                    var errorMessage = await paysResponse.Content.ReadAsStringAsync();
                    return CreateErrorView("Erreur lors de la rechargement de la liste des pays.", errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                return CreateErrorView("Erreur lors de la rechargement de la liste des pays.", ex.Message);
            }

            return View(voyage);
        }

        private IActionResult CreateErrorView(string title, string details = null)
        {
            _logger.LogError($"{title} Détails: {details}");
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = title,
                ErrorDetails = details
            };
            return View("Error", errorModel);
        }
    }
}

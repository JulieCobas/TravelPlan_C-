using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using Microsoft.Extensions.Logging;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    [Authorize]
    public class TransportsController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<TransportsController> _logger;
        private const string API_URL = "https://localhost:7287/api/Transports/";
        private const string RESERVATION_API_URL = "https://localhost:7287/api/Reservations/";

        public TransportsController(IHttpClientFactory httpClientFactory, ILogger<TransportsController> logger)
        {
            _client = httpClientFactory.CreateClient("default");
            _logger = logger;
        }

        // GET: Transports
        public async Task<IActionResult> Index(int voyageId)
        {
            try
            {
                var response = await _client.GetAsync(API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var transportDtos = JsonConvert.DeserializeObject<List<TransportDTO>>(json);
                    ViewData["VoyageId"] = voyageId; // Passing voyageId to the view
                    return View(transportDtos);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error in GET Transports action: {Message}", errorMessage);
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Transports action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        public async Task<IActionResult> Details(int id, int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var transport = JsonConvert.DeserializeObject<TransportDTO>(json);
                    ViewData["VoyageId"] = voyageId;
                    return View(transport);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error in GET Transport Details action: {Message}", errorMessage);
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in GET Transport Details action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmTransportSelection(short IdTransport, short voyageId, DateTime DateDebut, DateTime DateFin, bool BagageMain, bool BagageEnSoute, bool BagageLarge, bool Speedyboarding)
        {
            _logger.LogInformation("ConfirmTransportSelection called with IdTransport: {IdTransport}, voyageId: {voyageId}, DateDebut: {DateDebut}, DateFin: {DateFin}, BagageMain: {BagageMain}, BagageEnSoute: {BagageEnSoute}, BagageLarge: {BagageLarge}, Speedyboarding: {Speedyboarding}", IdTransport, voyageId, DateDebut, DateFin, BagageMain, BagageEnSoute, BagageLarge, Speedyboarding);

            if (voyageId == 0) voyageId = 1; // Default to 1 if voyageId is 0

            if (DateDebut == default(DateTime))
            {
                ModelState.AddModelError("DateDebut", "La date de début est requise.");
            }
            if (DateFin == default(DateTime))
            {
                ModelState.AddModelError("DateFin", "La date de fin est requise.");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state in ConfirmTransportSelection");
                // Recharger les données nécessaires pour la vue
                var response = await _client.GetAsync($"{API_URL}{IdTransport}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var transport = JsonConvert.DeserializeObject<TransportDTO>(json);
                    return View("Details", transport);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error in GET Transport action: {Message}", errorMessage);
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }

            try
            {
                var reservation = new ReservationDTO
                {
                    IdTransport = IdTransport,
                    IdVoyage = voyageId,
                    DateHeureDebut = DateDebut,
                    DateHeureFin = DateFin,
                    Disponibilite = true
                };

                _logger.LogInformation("Sending POST request to {Url} with data: {Data}", RESERVATION_API_URL, JsonConvert.SerializeObject(reservation));
                var reservationResponse = await _client.PostAsJsonAsync(RESERVATION_API_URL, reservation);
                if (reservationResponse.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Reservation created successfully.");
                    return RedirectToAction("Index", "Reservations");
                }

                var reservationErrorMessage = await reservationResponse.Content.ReadAsStringAsync();
                _logger.LogError("Error in POST Reservation action: {Message}", reservationErrorMessage);
                return RedirectToAction("Error", "Home", new { message = reservationErrorMessage });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error in POST ConfirmTransportSelection action: {Message}", ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message };
                return View("Error", errorModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProceedToLodging(short voyageId)
        {
            return RedirectToAction("Index", "Logements", new { voyageId });
        }
    }
}

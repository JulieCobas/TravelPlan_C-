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
        public async Task<IActionResult> ConfirmTransportSelection(TransportDTO transportDto, short voyageId)
        {
            if (voyageId == 0) voyageId = 1; // Default to 1 if voyageId is 0

            if (transportDto.DateDebut == default(DateTime))
            {
                ModelState.AddModelError("DateDebut", "La date de début est requise.");
            }
            if (transportDto.DateFin == default(DateTime))
            {
                ModelState.AddModelError("DateFin", "La date de fin est requise.");
            }
            if (!ModelState.IsValid)
            {
                // Recharger les données nécessaires pour la vue
                ViewData["SelectedCountry"] = Request.Form["country"];
                ViewData["VoyageId"] = voyageId;
                var response = await _client.GetAsync($"{API_URL}{transportDto.IdTransport}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var transport = JsonConvert.DeserializeObject<TransportDTO>(json);
                    return View("Details", transport);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }

            try
            {
                var response = await _client.GetAsync($"{API_URL}{transportDto.IdTransport}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("Error", "Home", new { message = errorMessage });
                }

                var json = await response.Content.ReadAsStringAsync();
                var transport = JsonConvert.DeserializeObject<TransportDTO>(json);

                transport.BagageMain = transportDto.BagageMain;
                transport.BagageEnSoute = transportDto.BagageEnSoute;
                transport.BagageLarge = transportDto.BagageLarge;
                transport.Speedyboarding = transportDto.Speedyboarding;

                var reservation = new ReservationDTO
                {
                    IdTransport = transportDto.IdTransport,
                    IdVoyage = voyageId,
                    DateHeureDebut = transportDto.DateDebut,
                    DateHeureFin = transportDto.DateFin,
                    Disponibilite = true
                };

                var reservationResponse = await _client.PostAsJsonAsync("https://localhost:7287/api/Reservations", reservation);
                if (reservationResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Reservations");
                }

                var reservationErrorMessage = await reservationResponse.Content.ReadAsStringAsync();
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

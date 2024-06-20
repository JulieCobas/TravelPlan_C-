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

        public async Task<IActionResult> Index(string country, int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}ByCountryName?countryName={country}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var transports = JsonConvert.DeserializeObject<List<TransportDTO>>(json);

                    ViewData["SelectedCountry"] = country;
                    ViewData["VoyageId"] = voyageId;
                    return View(transports);
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

        public async Task<IActionResult> Details(int id, string country, int voyageId)
        {
            try
            {
                var response = await _client.GetAsync($"{API_URL}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var transport = JsonConvert.DeserializeObject<TransportDTO>(json);
                    ViewData["SelectedCountry"] = country;
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
        public async Task<IActionResult> ConfirmTransportSelection(short IdTransport, short voyageId, DateTime DateDebut, DateTime DateFin, bool BagageMain, bool BagageEnSoute, bool BagageLarge, bool Speedyboarding)
        {
            if (DateDebut < DateTime.Now || DateFin < DateDebut)
            {
                ModelState.AddModelError("DateDebut", "Les dates sont invalides.");
                ModelState.AddModelError("DateFin", "Les dates sont invalides.");

                var response = await _client.GetAsync($"{API_URL}{IdTransport}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var transport = JsonConvert.DeserializeObject<TransportDTO>(json);
                    ViewData["VoyageId"] = voyageId;
                    ViewData["SelectedCountry"] = ViewData["SelectedCountry"];
                    return View("Details", transport);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error in GET Transport action: {Message}", errorMessage);
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }

            var reservation = new ReservationDTO
            {
                IdTransport = IdTransport,
                IdVoyage = voyageId,
                DateHeureDebut = DateDebut,
                DateHeureFin = DateFin,
                Disponibilite = true
            };

            try
            {
                var response = await _client.PostAsJsonAsync(RESERVATION_API_URL, reservation);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Reservations");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (HttpRequestException ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
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

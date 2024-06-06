using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;

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
        public async Task<IActionResult> Index(string country, int voyageId)
        {
            var response = await _client.GetAsync($"{API_URL}?country={country}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var transportDtos = JsonConvert.DeserializeObject<List<TransportDTO>>(json);
                ViewData["SelectedCountry"] = country;
                ViewData["VoyageId"] = voyageId; // Passing voyageId to the view
                return View(transportDtos);
            }

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Error", "Home", new { message = errorMessage });
        }

        // GET: Transports/Details/5
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
                    ViewData["VoyageId"] = voyageId == 0 ? 1 : voyageId; // Default to 1 if voyageId is 0
                    return View(transport);
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }
            catch (Exception ex)
            {
                // Handle exception
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmTransportSelection(short IdTransport, bool BagageMain, bool BagageEnSoute, bool BagageLarge, bool Speedyboarding, short voyageId, DateTime DateDebut, DateTime DateFin)
        {
            if (voyageId == 0) voyageId = 1; // Default to 1 if voyageId is 0

            var response = await _client.GetAsync($"{API_URL}{IdTransport}");
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", "Home", new { message = errorMessage });
            }

            var json = await response.Content.ReadAsStringAsync();
            var transportDto = JsonConvert.DeserializeObject<TransportDTO>(json);

            transportDto.BagageMain = BagageMain;
            transportDto.BagageEnSoute = BagageEnSoute;
            transportDto.BagageLarge = BagageLarge;
            transportDto.Speedyboarding = Speedyboarding;

            var reservation = new ReservationDTO
            {
                IdTransport = IdTransport,
                IdVoyage = voyageId,
                DateHeureDebut = DateDebut,
                DateHeureFin = DateFin,
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
    }
}

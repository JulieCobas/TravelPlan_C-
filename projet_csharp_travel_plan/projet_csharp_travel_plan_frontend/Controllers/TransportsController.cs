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
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            return View("Error", errorModel);
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(int id, int voyageId)
        {
            var response = await _client.GetAsync($"{API_URL}{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var transportDto = JsonConvert.DeserializeObject<TransportDTO>(json);
                ViewData["VoyageId"] = voyageId; // Passing voyageId to the view
                return View(transportDto);
            }

            // Handle error
            var errorMessage = await response.Content.ReadAsStringAsync();
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            return View("Error", errorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmTransportSelection(short IdTransport, bool BagageMain, bool BagageEnSoute, bool BagageLarge, bool Speedyboarding, short voyageId, DateTime DateDebut, DateTime DateFin)
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
            var reservationErrorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = reservationErrorMessage };
            return View("Error", reservationErrorModel);
        }

        // New action to proceed to lodging reservation without selecting transport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProceedToLodging(int voyageId)
        {
            return RedirectToAction("Index", "Logements", new { voyageId });
        }
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO; // Utilisation du DTO local au frontend

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Reservations/";

        public ReservationsController(HttpClient client)
        {
            _client = client;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync($"{API_URL}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservations = JsonConvert.DeserializeObject<List<ReservationDTO>>(json); // Utilisation du DTO local au frontend
                return View(reservations);
            }
            return View("Error");
        }

        // Autres actions comme Details, Create, Edit, etc.
    }

}

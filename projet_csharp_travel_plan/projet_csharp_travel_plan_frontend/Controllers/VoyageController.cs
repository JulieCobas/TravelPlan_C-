using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class VoyageController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Voyages/";
        private const string PAYS_API_URL = "https://localhost:7287/api/Pay/";

        public VoyageController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("default");
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
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            return View("Error", errorModel);
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
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
            return View("Error", errorModel);
        }

        // POST: Voyage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoyageDTO voyage)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(voyage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(API_URL, content);
               if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Create", "Reservations", new { voyage.IdPays, voyage.IdVoyage });
                }

                // Handle error
                var errorMessage = await response.Content.ReadAsStringAsync();
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage };
                return View("Error", errorModel);
            }

            // Si la validation échoue, rechargez la liste des pays
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

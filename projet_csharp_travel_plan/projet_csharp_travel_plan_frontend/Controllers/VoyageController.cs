
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class VoyageController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Voyages/";

        public VoyageController(HttpClient client)
        {
            _client = client;
        }

        // GET: Voyage
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(API_URL);
            List<VoyageDTO> voyageDtos = new List<VoyageDTO>();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                voyageDtos = JsonConvert.DeserializeObject<List<VoyageDTO>>(json);
            }
            else
            {
                return View("Error");
            }

            return View(voyageDtos);
        }

        // GET: Voyage/Create
        public async Task<IActionResult> Create()
        {
            var paysResponse = await _client.GetAsync($"{API_URL}Pays");
            List<PayDTO> pays = new List<PayDTO>();

            if (paysResponse.IsSuccessStatusCode)
            {
                var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                pays = JsonConvert.DeserializeObject<List<PayDTO>>(jsonPays);
            }

            var voyageDto = new VoyageDTO
            {
                DateDebut = DateOnly.FromDateTime(DateTime.Now),
                DateFin = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                Pays = pays
            };

            return View(voyageDto);
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
                    return RedirectToAction("Index");
                }
            }

            // Si la validation échoue, rechargez la liste des pays
            var paysResponse = await _client.GetAsync($"{API_URL}Pays");
            List<PayDTO> pays = new List<PayDTO>();

            if (paysResponse.IsSuccessStatusCode)
            {
                var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                pays = JsonConvert.DeserializeObject<List<PayDTO>>(jsonPays);
            }

            voyage.Pays = pays;
            return View(voyage);
        }
    }
}
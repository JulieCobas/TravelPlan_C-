using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace projet_csharp_travel_plan_frontend.Controllers
{
    public class VoyageController : Controller
    {
        private readonly HttpClient _client;
        private const string API_URL = "https://localhost:7287/api/Voyage";

        public VoyageController(HttpClient client)
        {
            _client = client;
        }

        // GET: Voyages/Create
        // Afficher le formulaire pour la création d'un voyage
        public async Task<IActionResult> Create()
        {
            // Optionnellement, récupérer des données initiales comme la liste des pays
            var paysResponse = await _client.GetAsync($"{API_URL}Pays");
            List<Pay> pays = new List<Pay>();

            if (paysResponse.IsSuccessStatusCode)
            {
                var jsonPays = await paysResponse.Content.ReadAsStringAsync();
                pays = JsonConvert.DeserializeObject<List<Pay>>(jsonPays);
            }

            ViewBag.Pays = pays.Select(p => new { Id = p.IdPays, Name = p.Nom }).ToList();
            return View();
        }

        // POST: Voyages/Create
        // Traitement des informations saisies pour créer un nouveau voyage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(voyage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{API_URL}Voyages", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Redirection vers la liste des voyages après création
                }
            }

            // En cas d'échec, afficher de nouveau le formulaire avec les données saisies
            return View(voyage);
        }

        // GET: Voyages/Index
        // Liste tous les voyages
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync($"{API_URL}Voyages");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var voyages = JsonConvert.DeserializeObject<List<Voyage>>(json);
                return View(voyages);
            }
            return View("Error");
        }

        // GET: Voyages/Details/5
        // Affiche les détails d'un voyage spécifique
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{API_URL}Voyages/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var voyage = JsonConvert.DeserializeObject<Voyage>(json);
                return View(voyage);
            }
            return View("Error");
        }
    }
}

//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using projet_csharp_travel_plan_frontend.Models;

//namespace projet_csharp_travel_plan_frontend.Controllers
//{
//    public class ActivitesController : Controller
//    {
//        private readonly HttpClient _client;
//        private const string API_URL = "https://localhost:7287/api/Logements/";

//        public ActivitesController(HttpClient client)
//        {
//            _client = client;
//        }

//        public async Task<IActionResult> Index(int? idPays)
//        {
//            // Vérifie si l'ID du pays est fourni
//            if (idPays == null)
//            {
//                return BadRequest(); // Retourne un code d'erreur si l'ID du pays n'est pas fourni
//            }

//            // Récupère les logements du pays spécifié avec les données des fournisseurs, des catégories de logements et des pays incluses
//            var response = await _client.GetAsync($"{API_URL}?idPays={idPays}&includeFournisseurs=true&includeCategories=true&includePays=true");
//            if (response.IsSuccessStatusCode)
//            {
//                var json = await response.Content.ReadAsStringAsync();
//                var activites = JsonConvert.DeserializeObject<List<Activite>>(json);
//                return View(activites);
//            }
//            return View("Error");
//        }


//        // GET: Logements/Details/5
//        public async Task<IActionResult> Details(int id)
//        {
//            var response = await _client.GetAsync($"{API_URL}{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var json = await response.Content.ReadAsStringAsync();
//                var activite = JsonConvert.DeserializeObject<Activite>(json);
//                return View(activite);
//            }
//            return View("Error");
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projet_csharp_travel_plan_frontend.DTO;
using System.Collections.Generic;
using System.Net.Http;
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
            try
            {
                var response = await _client.GetAsync($"{API_URL}Pays");
                List<PayDTO> pays = new List<PayDTO>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    pays = JsonConvert.DeserializeObject<List<PayDTO>>(json);
                }
                else
                {
                    return View("Error");
                }

                var voyageDto = new VoyageDTO
                {
                    DateDebut = DateOnly.FromDateTime(DateTime.Now),
                    DateFin = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                    Pays = pays
                };

                return View(voyageDto);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if needed
                return View("Error");
            }
        }
    }
}
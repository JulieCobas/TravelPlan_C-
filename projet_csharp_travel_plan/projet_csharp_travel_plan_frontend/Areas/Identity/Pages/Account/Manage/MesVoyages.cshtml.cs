using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projet_csharp_travel_plan_frontend.DTO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace projet_csharp_travel_plan_frontend.Areas.Identity.Pages.Account.Manage
{
    public class MesVoyagesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MesVoyagesModel> _logger;
        private const string API_URL = "https://localhost:7287/api";
        private const string API_KEY = "test"; 

        public MesVoyagesModel(UserManager<IdentityUser> userManager, IHttpClientFactory httpClientFactory, ILogger<MesVoyagesModel> logger)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IList<VoyageDTO> Voyages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("User not logged in or not found");
                return RedirectToPage("/Account/Login");
            }

            var clientId = await GetClientIdAsync(user.Id);
            if (!clientId.HasValue)
            {
                _logger.LogWarning("No client associated with this user.");
                return Page();
            }

            await LoadVoyagesAsync(clientId.Value);
            return Page();
        }

        private async Task<short?> GetClientIdAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient("Default");
            client.DefaultRequestHeaders.Add("x-api-key", API_KEY);

            var response = await client.GetFromJsonAsync<ClientDTO>($"{API_URL}/clients/{userId}");
            if (response == null)
            {
                return null;
            }
            return response.IdClient;
        }

        private async Task LoadVoyagesAsync(short clientId)
        {
            var client = _httpClientFactory.CreateClient("Default");
            client.DefaultRequestHeaders.Add("x-api-key", API_KEY);

            Voyages = await client.GetFromJsonAsync<List<VoyageDTO>>($"{API_URL}/voyages/voyagesbyclient/{clientId}");
        }
    } 
}

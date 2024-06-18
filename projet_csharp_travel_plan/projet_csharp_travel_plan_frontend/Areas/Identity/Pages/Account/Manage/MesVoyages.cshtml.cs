using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projet_csharp_travel_plan_frontend.DTO;

namespace projet_csharp_travel_plan_frontend.Areas.Identity.Pages.Account.Manage
{
    public class MesVoyagesModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProfilModel> _logger;
        private const string API_URL = "https://localhost:7287/api/voyages";
        private const string API_KEY = "test";
        private ClientDTO _clientDTO;

        public MesVoyagesModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpClientFactory httpClientFactory, ILogger<ProfilModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClientFactory.CreateClient("default");
            _logger = logger;
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);

            // Load client data from API
            _logger.LogInformation("Loading client data for user ID: {UserId}", userId);

            var request = new HttpRequestMessage(HttpMethod.Get, $"{API_URL}/{userId}");
            request.Headers.Add("Authorization", $"Bearer {API_KEY}");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                _clientDTO = await response.Content.ReadFromJsonAsync<ClientDTO>();





            }


        public void OnGet()
        {
        }
    }
}

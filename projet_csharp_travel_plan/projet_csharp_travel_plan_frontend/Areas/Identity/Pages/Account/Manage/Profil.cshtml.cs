using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using projet_csharp_travel_plan_frontend.DTO;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace projet_csharp_travel_plan_frontend.Areas.Identity.Pages.Account.Manage
{
    public class ProfilModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProfilModel> _logger;
        private const string API_URL = "https://localhost:7287/api/clients";

        public ProfilModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpClient httpClient, ILogger<ProfilModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClient;
            _logger = logger;
        }

        public string Username { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        [BindProperty]
        public ClientDTO Client { get; set; } = new ClientDTO();

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);


            // Load client data from API
            _logger.LogInformation("Loading client data for user ID: {UserId}", user.Id);
            var response = await _httpClient.GetAsync($"{API_URL}/{user.Id}");
            if (response.IsSuccessStatusCode)
            {
                Client = await response.Content.ReadFromJsonAsync<ClientDTO>();
                if (Client == null)
                {
                    _logger.LogWarning("Client data not found for user ID: {UserId}", user.Id);
                    Client = new ClientDTO();
                }
                else
                {
                    _logger.LogInformation("Client data loaded successfully for user ID: {UserId}", user.Id);
                    if (Client.DateNaissance != default)
                    {
                        Day = Client.DateNaissance.Day;
                        Month = Client.DateNaissance.Month;
                        Year = Client.DateNaissance.Year;
                    }
                }
            }
            else
            {
                _logger.LogError("Error loading client data for user ID: {UserId}. Status Code: {StatusCode}", user.Id, response.StatusCode);
                Client = new ClientDTO();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            try
            {
                Client.DateNaissance = new DateOnly(Year, Month, Day);
            }
            catch (ArgumentOutOfRangeException)
            {
                ModelState.AddModelError(string.Empty, "Invalid date.");
                await LoadAsync(user);
                return Page();
            }

            Client.Id = user.Id;

            var response = await _httpClient.PutAsJsonAsync($"{API_URL}/{Client.IdClient}", Client);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error updating profile.");
                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
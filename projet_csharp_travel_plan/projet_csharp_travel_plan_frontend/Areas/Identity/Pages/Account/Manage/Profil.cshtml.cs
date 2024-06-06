using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using projet_csharp_travel_plan_frontend.DTO;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace projet_csharp_travel_plan_frontend.Areas.Identity.Pages.Account.Manage
{
    public class ProfilModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProfilModel> _logger;
        private const string API_URL = "https://localhost:7287/api/clients";
        private const string API_KEY = "test";

        public ProfilModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpClient httpClient, ILogger<ProfilModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClient;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public ClientDTO Client { get; set; } = new ClientDTO();

        public class InputModel
        {
            [Required]
            public int Day { get; set; }
            [Required]
            public int Month { get; set; }
            [Required]
            public int Year { get; set; }
            [Required]
            [Display(Name = "Nom")]
            public string Nom { get; set; }
            [Required]
            [Display(Name = "Prénom")]
            public string Prenom { get; set; }
            [Required]
            [Display(Name = "Adresse")]
            public string Adresse { get; set; }
            [Required]
            [Display(Name = "Code postal")]
            public string CodePostal { get; set; }
            [Required]
            [Display(Name = "Ville")]
            public string Ville { get; set; }
            [Required]
            [Display(Name = "Pays")]
            public string Pays { get; set; }
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
                Client = await response.Content.ReadFromJsonAsync<ClientDTO>();

                if (Client == null)
                {
                    _logger.LogWarning("Client data not found for user ID: {UserId}", userId);
                    Client = new ClientDTO();
                }
                else
                {
                    _logger.LogInformation("Client data loaded successfully for user ID: {UserId}", userId);
                    if (Client.DateNaissance != default)
                    {
                        Input = new InputModel
                        {
                            Nom = Client.Nom,
                            Prenom = Client.Prenom,
                            Adresse = Client.Addresse,
                            CodePostal = Client.Cp,
                            Ville = Client.Ville,
                            Pays = Client.Pays,
                            Day = Client.DateNaissance.Day,
                            Month = Client.DateNaissance.Month,
                            Year = Client.DateNaissance.Year
                        };
                    }
                }
            }
            else
            {
                _logger.LogError("Error loading client data for user ID: {UserId}. Status Code: {StatusCode}", userId, response.StatusCode);
                Client = new ClientDTO();
            }
        }

        private async Task<IActionResult> CreateClientAsync(IdentityUser user)
        {
            Client.Id = await _userManager.GetUserIdAsync(user);
            Client.Nom = Input.Nom;
            Client.Prenom = Input.Prenom;
            Client.Addresse = Input.Adresse;
            Client.Cp = Input.CodePostal;
            Client.Ville = Input.Ville;
            Client.Pays = Input.Pays;
            Client.DateNaissance = new DateOnly(Input.Year, Input.Month, Input.Day);

            var request = new HttpRequestMessage(HttpMethod.Post, API_URL)
            {
                Content = JsonContent.Create(Client)
            };
            request.Headers.Add("Authorization", $"Bearer {API_KEY}");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error creating profile.");
                return Page();
            }

            return RedirectToPage();
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

            var userId = await _userManager.GetUserIdAsync(user);
            var request = new HttpRequestMessage(HttpMethod.Get, $"{API_URL}/{userId}");
            request.Headers.Add("Authorization", $"Bearer {API_KEY}");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Client = await response.Content.ReadFromJsonAsync<ClientDTO>();

                if (Client == null)
                {
                    _logger.LogWarning("Client data not found for user ID: {UserId}", userId);
                    return await CreateClientAsync(user);
                }

                try
                {
                    Client.DateNaissance = new DateOnly(Input.Year, Input.Month, Input.Day);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ModelState.AddModelError(string.Empty, "Invalid date.");
                    await LoadAsync(user);
                    return Page();
                }

                Client.Nom = Input.Nom;
                Client.Prenom = Input.Prenom;
                Client.Addresse = Input.Adresse;
                Client.Cp = Input.CodePostal;
                Client.Ville = Input.Ville;
                Client.Pays = Input.Pays;

                request = new HttpRequestMessage(HttpMethod.Put, $"{API_URL}/{Client.IdClient}")
                {
                    Content = JsonContent.Create(Client)
                };
                request.Headers.Add("Authorization", $"Bearer {API_KEY}");

                response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Error updating profile.");
                    await LoadAsync(user);
                    return Page();
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return await CreateClientAsync(user);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error loading client data.");
                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

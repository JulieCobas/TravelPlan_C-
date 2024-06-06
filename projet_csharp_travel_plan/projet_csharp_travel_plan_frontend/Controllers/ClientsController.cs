/*using Microsoft.AspNetCore.Mvc;
using projet_csharp_travel_plan_frontend.DTO;
using projet_csharp_travel_plan_frontend.Services;
using Microsoft.AspNetCore.Identity;

namespace projet_csharp_travel_plan_frontend.Controllers
{
        public class ClientsController : Controller
        {
            private readonly ClientApiService _clientApiService;
            private readonly UserManager<IdentityUser> _userManager;

            public ClientsController(ClientApiService clientApiService, UserManager<IdentityUser> userManager)
            {
                _clientApiService = clientApiService;
                _userManager = userManager;
            }

            [HttpGet]
            public async Task<IActionResult> Profile()
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound("Unable to load user.");
                }

                var client = await _clientApiService.GetClientAsync(user.Id);
                if (client == null)
                {
                    return NotFound("Unable to load client.");
                }

                return View(client);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Profile(ClientDTO clientDto)
            {
                if (!ModelState.IsValid)
                {
                    return View(clientDto);
                }

                try
                {
                    await _clientApiService.UpdateClientAsync(clientDto);
                    TempData["StatusMessage"] = "Profile updated successfully.";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error updating profile: {ex.Message}");
                    return View(clientDto);
                }

                return RedirectToAction(nameof(Profile));            
        }
        
    }
    
}

*/
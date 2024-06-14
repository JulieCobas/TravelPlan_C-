using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly TravelPlanNewDbContext _context;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(TravelPlanNewDbContext context, ILogger<ClientsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ClientDTO>> GetClient(string userId)
    {
        _logger.LogInformation("Fetching client data for user ID: {UserId}", userId);

        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == userId);
        if (client == null)
        {
            _logger.LogWarning($"Client not found for user ID: {userId}");
            return NotFound();
        }

        var clientDto = new ClientDTO
        {
            IdClient = client.IdClient,
            Id = client.Id,
            Addresse = client.Addresse,
            Cp = client.Cp,
            Ville = client.Ville,
            Pays = client.Pays,
            Nom = client.Nom,
            Prenom = client.Prenom,
            DateNaissance = client.DateNaissance,
        };

        _logger.LogInformation($"Client data successfully fetched for user ID: {userId}");
        return Ok(clientDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, ClientDTO clientDto)
    {
        if (id != clientDto.IdClient)
        {
            _logger.LogWarning($"Bad request for client update. ID mismatch: {id}");
            return BadRequest("Client ID mismatch.");
        }

        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == id);
        if (client == null)
        {
            _logger.LogWarning($"Client not found for ID: {id}");
            return NotFound();
        }

        // Update client properties
        client.Id = clientDto.Id;
        client.Addresse = clientDto.Addresse;
        client.Cp = clientDto.Cp;
        client.Ville = clientDto.Ville;
        client.Pays = clientDto.Pays;
        client.Nom = clientDto.Nom;
        client.Prenom = clientDto.Prenom;
        client.DateNaissance = clientDto.DateNaissance;

        try
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Client data successfully updated for ID: {id}");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!ClientExists(id))
            {
                _logger.LogError(ex, $"Client not found during update for ID: {id}");
                return NotFound();
            }
            else
            {
                _logger.LogError(ex, $"Concurrency error during client update for ID: {id}");
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating client data for ID: {id}");
            return StatusCode(500, "An error occurred while updating the client.");
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientDTO clientDto)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == clientDto.Id);
        if (client != null)
        {
            return Conflict("Client already exists.");
        }

        var newClient = new Client
        {
            IdClient = clientDto.IdClient,
            Id = clientDto.Id,
            Addresse = clientDto.Addresse,
            Cp = clientDto.Cp,
            Ville = clientDto.Ville,
            Pays = clientDto.Pays,
            Nom = clientDto.Nom,
            Prenom = clientDto.Prenom,
            DateNaissance = clientDto.DateNaissance
        };

        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClient), new { userId = newClient.Id }, clientDto);
    }

    [HttpGet("maxid")]
    public async Task<ActionResult<int>> GetMaxClientId()
    {
        var maxId = await _context.Clients.MaxAsync(c => (int?)c.IdClient) ?? 0;
        return Ok(maxId);
    }

    private bool ClientExists(int id)
    {
        return _context.Clients.Any(e => e.IdClient == id);
    }
}

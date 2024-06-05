using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;



[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly TravelPlanNewDbContext _context;

    public ClientsController(TravelPlanNewDbContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ClientDTO>> GetClient(string userId)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == userId);
        if (client == null)
        {
            return NotFound();
        }

        return Ok(new ClientDTO
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
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, ClientDTO clientDto)
    {
        if (id != clientDto.IdClient)
        {
            return BadRequest();
        }

        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == id);
        if (client == null)
        {
            return NotFound();
        }

        // Ensure the current user's ID is correctly set as the foreign key
        client.Id = clientDto.Id;
        client.Addresse = clientDto.Addresse;
        client.Cp = clientDto.Cp;
        client.Ville = clientDto.Ville;
        client.Pays = clientDto.Pays;
        client.Nom = clientDto.Nom;
        client.Prenom = clientDto.Prenom;
        client.DateNaissance = clientDto.DateNaissance;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

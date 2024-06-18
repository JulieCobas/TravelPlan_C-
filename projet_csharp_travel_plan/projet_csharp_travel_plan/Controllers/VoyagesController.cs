using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class VoyagesController : ControllerBase
{
    private readonly TravelPlanNewDbContext _context;

    public VoyagesController(TravelPlanNewDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VoyageDTO>>> GetVoyages()
    {
        var voyages = await _context.Voyages
            .Select(v => new VoyageDTO
            {
                IdVoyage = v.IdVoyage,
                DateDebut = v.DateDebut,
                DateFin = v.DateFin,
                IdClient = (short)v.IdClient,
                PrixTotal = v.PrixTotal,
                IdPays = v.IdPays.FirstOrDefault().IdPays // Sélectionne le premier ID de pays
            })
            .ToListAsync();

        return voyages;
    }

    [HttpGet("GetPays")]
    public async Task<ActionResult<IEnumerable<PayDTO>>> GetPays()
    {
        var pays = await _context.Pays
            .Select(p => new PayDTO
            {
                IdPays = p.IdPays,
                Nom = p.Nom
            })
            .ToListAsync();

        return pays;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VoyageDTO>> GetVoyage(int id)
    {
        var voyage = await _context.Voyages
            .Where(v => v.IdVoyage == id)
            .Select(v => new VoyageDTO
            {
                IdVoyage = v.IdVoyage,
                DateDebut = v.DateDebut,
                DateFin = v.DateFin,
                IdPays = v.IdPays.FirstOrDefault().IdPays // Sélectionne le premier ID de pays
            })
            .FirstOrDefaultAsync();

        if (voyage == null)
        {
            return NotFound();
        }

        return voyage;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutVoyage(int id, VoyageDTO dto)
    {
        if (id != dto.IdVoyage)
        {
            return BadRequest();
        }

        var voyage = await _context.Voyages.Include(v => v.IdPays).FirstOrDefaultAsync(v => v.IdVoyage == id);
        if (voyage == null)
        {
            return NotFound();
        }

        voyage.DateDebut = dto.DateDebut;
        voyage.DateFin = dto.DateFin;

        // Mettre à jour la relation avec le pays
        voyage.IdPays.Clear();
        var existingPay = await _context.Pays.FindAsync(dto.IdPays);
        if (existingPay != null)
        {
            voyage.IdPays.Add(existingPay);
        }

        _context.Entry(voyage).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VoyageExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<VoyageDTO>> PostVoyage(VoyageDTO dto)
    {
        if (dto == null)
        {
            return BadRequest("Le voyage à ajouter est null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingClient = await _context.Clients.FindAsync(dto.IdClient);
        if (existingClient == null)
        {
            return NotFound($"Client with Id: {dto.IdClient} not found");
        }

        var existingPay = await _context.Pays.FindAsync(dto.IdPays);
        if (existingPay == null)
        {
            return NotFound($"Pay with Id: {dto.IdPays} not found");
        }

        var voyage = new Voyage
        {
            DateDebut = dto.DateDebut,
            DateFin = dto.DateFin,
            IdClient = dto.IdClient,
            PrixTotal = dto.PrixTotal,
            StatutPaiement = dto.StatutPaiement,
            IdClientNavigation = existingClient
        };

        _context.Voyages.Add(voyage);

        try
        {
            await _context.SaveChangesAsync();
            dto.IdVoyage = voyage.IdVoyage;

            return CreatedAtAction(nameof(GetVoyage), new { id = voyage.IdVoyage }, dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [HttpGet("GetByUserId")]
    public async Task<ActionResult<ClientDTO>> GetClientByUserId(string userId)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == userId);
        if (client == null)
        {
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
            DateNaissance = client.DateNaissance
        };

        return clientDto;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVoyage(int id)
    {
        var voyage = await _context.Voyages.FindAsync(id);
        if (voyage == null)
        {
            return NotFound();
        }

        _context.Voyages.Remove(voyage);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VoyageExists(int id)
    {
        return _context.Voyages.Any(e => e.IdVoyage == id);
    }

    [HttpGet("VoyagesByClient/{clientId}")]
    public async Task<ActionResult<IEnumerable<VoyageDTO>>> GetVoyagesByClient(short clientId)
    {
        var voyages = await _context.Voyages
            .Where(v => v.IdClient == clientId)
            .OrderBy(v => v.DateDebut) // Trier par DateDebut
            .Select(v => new VoyageDTO
            {
                IdVoyage = v.IdVoyage,
                DateDebut = v.DateDebut,
                DateFin = v.DateFin,
                PrixTotal = v.PrixTotal
            })
            .ToListAsync();

        if (voyages == null || !voyages.Any())
        {
            return NotFound("No voyages found for the client.");
        }

        return Ok(voyages);
    }



}




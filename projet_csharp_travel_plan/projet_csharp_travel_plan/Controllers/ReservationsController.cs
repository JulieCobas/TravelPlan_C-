using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly TravelPlanNewDbContext _context;

    public ReservationsController(TravelPlanNewDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
    {
        var reservations = await _context.Reservations
            .Include(r => r.IdLogementNavigation)
            .Include(r => r.IdActiviteNavigation)
            .Include(r => r.IdTransportNavigation)
            .Include(r => r.IdVoyageNavigation)
            .ThenInclude(v => v.IdPays)
            .Select(r => new ReservationDTO
            {
                IdReservation = r.IdReservation,
                IdLogement = r.IdLogement,
                IdActivite = r.IdActivite,
                IdTransport = r.IdTransport,
                IdVoyage = r.IdVoyage,
                DateHeureDebut = r.DateHeureDebut,
                DateHeureFin = r.DateHeureFin,
                Disponibilite = r.Disponibilite,
                Logement = r.IdLogementNavigation != null ? new LogementDTO
                {
                    IdLogement = r.IdLogementNavigation.IdLogement,
                    Nom = r.IdLogementNavigation.Nom,
                    Details = r.IdLogementNavigation.Details,
                    Note = r.IdLogementNavigation.Note,
                    NbEvaluation = r.IdLogementNavigation.NbEvaluation,
                    Img = r.IdLogementNavigation.Img,
                    Disponibilite = r.IdLogementNavigation.Disponibilite,
                    NomFournisseur = r.IdLogementNavigation.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = r.IdLogementNavigation.IdLogementCategorieNavigation.Nom,
                    NomPays = r.IdLogementNavigation.IdPaysNavigation.Nom
                } : null,
                Activite = r.IdActiviteNavigation != null ? new ActiviteDTO
                {
                    IdActivite = r.IdActiviteNavigation.IdActivite,
                    Nom = r.IdActiviteNavigation.Nom,
                    Details = r.IdActiviteNavigation.Details,
                    Note = r.IdActiviteNavigation.Note,
                    NbEvaluation = r.IdActiviteNavigation.NbEvaluation,
                    HeuresMoyennes = r.IdActiviteNavigation.HeuresMoyennes,
                    Img = r.IdActiviteNavigation.Img,
                    CapaciteMax = r.IdActiviteNavigation.CapaciteMax,
                    NomCategorie = r.IdActiviteNavigation.IdCatActivNavigation.Nom,
                    NomFournisseur = r.IdActiviteNavigation.IdFournisseurNavigation.NomCompagnie,
                    NomPays = r.IdActiviteNavigation.IdPaysNavigation.Nom,
                    PrixActivite = r.IdActiviteNavigation.IdPrixActiviteNavigation.Prix,
                    GuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation.GuideAudio,
                    PrixGuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixGuideAudio,
                    VisiteGuidee = r.IdActiviteNavigation.IdOptionActiviteNavigation.VisiteGuidee,
                    PrixVisiteGuide = r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixVisiteGuide
                } : null,
                Transport = r.IdTransportNavigation != null ? new TransportDTO
                {
                    IdTransport = r.IdTransportNavigation.IdTransport,
                    NomFournisseur = r.IdTransportNavigation.IdFournisseurNavigation.NomCompagnie,
                    BagageMain = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageMain,
                    BagageEnSoute = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageEnSoute,
                    BagageLarge = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageLarge,
                    Speedyboarding = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.Speedyboarding,
                    Prix = r.IdTransportNavigation.IdPrixTransportNavigation.Prix,
                    CategorieTransportNom = r.IdTransportNavigation.IdCategorieTransportNavigation.Nom
                } : null,
                Voyage = r.IdVoyageNavigation != null ? new VoyageDTO
                {
                    IdVoyage = r.IdVoyageNavigation.IdVoyage,
                    DateDebut = r.IdVoyageNavigation.DateDebut,
                    DateFin = r.IdVoyageNavigation.DateFin,
                    IdClient = r.IdVoyageNavigation.IdClient ?? 0,
                    PrixTotal = r.IdVoyageNavigation.PrixTotal,

                    //IdPays = r.IdVoyageNavigation.IdPays.FirstOrDefault().IdPays, // Obtenez le premier IdPays

                } : null
            })
            .ToListAsync();

        return reservations;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
    {
        var reservation = await _context.Reservations
            .Include(r => r.IdLogementNavigation)
            .Include(r => r.IdActiviteNavigation)
            .Include(r => r.IdTransportNavigation)
            .Include(r => r.IdVoyageNavigation)
            .Where(r => r.IdReservation == id)
            .Select(r => new ReservationDTO
            {
                IdReservation = r.IdReservation,
                IdLogement = r.IdLogement,
                IdActivite = r.IdActivite,
                IdTransport = r.IdTransport,
                IdVoyage = r.IdVoyage,
                DateHeureDebut = r.DateHeureDebut,
                DateHeureFin = r.DateHeureFin,
                Disponibilite = r.Disponibilite,
                Logement = r.IdLogementNavigation != null ? new LogementDTO
                {
                    IdLogement = r.IdLogementNavigation.IdLogement,
                    Nom = r.IdLogementNavigation.Nom,
                    Details = r.IdLogementNavigation.Details,
                    Note = r.IdLogementNavigation.Note,
                    NbEvaluation = r.IdLogementNavigation.NbEvaluation,
                    Img = r.IdLogementNavigation.Img,
                    Disponibilite = r.IdLogementNavigation.Disponibilite,
                    NomFournisseur = r.IdLogementNavigation.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = r.IdLogementNavigation.IdLogementCategorieNavigation.Nom,
                    NomPays = r.IdLogementNavigation.IdPaysNavigation.Nom
                } : null,
                Activite = r.IdActiviteNavigation != null ? new ActiviteDTO
                {
                    IdActivite = r.IdActiviteNavigation.IdActivite,
                    Nom = r.IdActiviteNavigation.Nom,
                    Details = r.IdActiviteNavigation.Details,
                    Note = r.IdActiviteNavigation.Note,
                    NbEvaluation = r.IdActiviteNavigation.NbEvaluation,
                    HeuresMoyennes = r.IdActiviteNavigation.HeuresMoyennes,
                    Img = r.IdActiviteNavigation.Img,
                    CapaciteMax = r.IdActiviteNavigation.CapaciteMax,
                    NomCategorie = r.IdActiviteNavigation.IdCatActivNavigation.Nom,
                    NomFournisseur = r.IdActiviteNavigation.IdFournisseurNavigation.NomCompagnie,
                    NomPays = r.IdActiviteNavigation.IdPaysNavigation.Nom,
                    PrixActivite = r.IdActiviteNavigation.IdPrixActiviteNavigation.Prix,
                    GuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation.GuideAudio,
                    PrixGuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixGuideAudio,
                    VisiteGuidee = r.IdActiviteNavigation.IdOptionActiviteNavigation.VisiteGuidee,
                    PrixVisiteGuide = r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixVisiteGuide
                } : null,
                Transport = r.IdTransportNavigation != null ? new TransportDTO
                {
                    IdTransport = r.IdTransportNavigation.IdTransport,
                    NomFournisseur = r.IdTransportNavigation.IdFournisseurNavigation.NomCompagnie,
                    BagageMain = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageMain,
                    BagageEnSoute = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageEnSoute,
                    BagageLarge = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageLarge,
                    Speedyboarding = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.Speedyboarding,
                    Prix = r.IdTransportNavigation.IdPrixTransportNavigation.Prix,
                    CategorieTransportNom = r.IdTransportNavigation.IdCategorieTransportNavigation.Nom
                } : null

            })
            .FirstOrDefaultAsync();

        if (reservation == null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReservation(int id, ReservationDTO dto)
    {
        if (id != dto.IdReservation)
        {
            return BadRequest();
        }

        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }

        reservation.IdLogement = dto.IdLogement;
        reservation.IdActivite = dto.IdActivite;
        reservation.IdTransport = dto.IdTransport;
        reservation.IdVoyage = dto.IdVoyage;
        reservation.DateHeureDebut = dto.DateHeureDebut;
        reservation.DateHeureFin = dto.DateHeureFin;
        reservation.Disponibilite = dto.Disponibilite;
        // Update other fields if necessary

        _context.Entry(reservation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReservationExists(id))
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
    public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO dto)
    {
        var reservation = new Reservation
        {
            IdLogement = dto.IdLogement,
            IdActivite = dto.IdActivite,
            IdTransport = dto.IdTransport,
            IdVoyage = dto.IdVoyage,
            DateHeureDebut = dto.DateHeureDebut,
            DateHeureFin = dto.DateHeureFin,
            Disponibilite = dto.Disponibilite
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        dto.IdReservation = reservation.IdReservation;

        return CreatedAtAction(nameof(GetReservation), new { id = reservation.IdReservation }, dto);
    }

    private bool ReservationExists(int id)
    {
        return _context.Reservations.Any(e => e.IdReservation == id);
    }

    [HttpGet("byvoyage/{voyageId}")]
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsByVoyage(int voyageId)
    {
        var reservations = await _context.Reservations
            .Where(r => r.IdVoyage == voyageId)
            .Include(r => r.IdLogementNavigation)
            .Include(r => r.IdActiviteNavigation)
            .Include(r => r.IdTransportNavigation)
            .Include(r => r.IdVoyageNavigation)
            .ThenInclude(v => v.IdPays)
            .Select(r => new ReservationDTO
            {
                IdReservation = r.IdReservation,
                IdLogement = r.IdLogement,
                IdActivite = r.IdActivite,
                IdTransport = r.IdTransport,
                IdVoyage = r.IdVoyage,
                DateHeureDebut = r.DateHeureDebut,
                DateHeureFin = r.DateHeureFin,
                Disponibilite = r.Disponibilite,
                Logement = r.IdLogementNavigation != null ? new LogementDTO
                {
                    IdLogement = r.IdLogementNavigation.IdLogement,
                    Nom = r.IdLogementNavigation.Nom,
                    Details = r.IdLogementNavigation.Details,
                    Note = r.IdLogementNavigation.Note,
                    NbEvaluation = r.IdLogementNavigation.NbEvaluation,
                    Img = r.IdLogementNavigation.Img,
                    Disponibilite = r.IdLogementNavigation.Disponibilite,
                    NomFournisseur = r.IdLogementNavigation.IdFournisseurNavigation.NomCompagnie,
                    NomCategorie = r.IdLogementNavigation.IdLogementCategorieNavigation.Nom,
                    NomPays = r.IdLogementNavigation.IdPaysNavigation.Nom
                } : null,
                Activite = r.IdActiviteNavigation != null ? new ActiviteDTO
                {
                    IdActivite = r.IdActiviteNavigation.IdActivite,
                    Nom = r.IdActiviteNavigation.Nom,
                    Details = r.IdActiviteNavigation.Details,
                    Note = r.IdActiviteNavigation.Note,
                    NbEvaluation = r.IdActiviteNavigation.NbEvaluation,
                    HeuresMoyennes = r.IdActiviteNavigation.HeuresMoyennes,
                    Img = r.IdActiviteNavigation.Img,
                    CapaciteMax = r.IdActiviteNavigation.CapaciteMax,
                    NomCategorie = r.IdActiviteNavigation.IdCatActivNavigation.Nom,
                    NomFournisseur = r.IdActiviteNavigation.IdFournisseurNavigation.NomCompagnie,
                    NomPays = r.IdActiviteNavigation.IdPaysNavigation.Nom,
                    PrixActivite = r.IdActiviteNavigation.IdPrixActiviteNavigation.Prix,
                    GuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation.GuideAudio,
                    PrixGuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixGuideAudio,
                    VisiteGuidee = r.IdActiviteNavigation.IdOptionActiviteNavigation.VisiteGuidee,
                    PrixVisiteGuide = r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixVisiteGuide
                } : null,
                Transport = r.IdTransportNavigation != null ? new TransportDTO
                {
                    IdTransport = r.IdTransportNavigation.IdTransport,
                    NomFournisseur = r.IdTransportNavigation.IdFournisseurNavigation.NomCompagnie,
                    BagageMain = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageMain,
                    BagageEnSoute = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageEnSoute,
                    BagageLarge = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.BagageLarge,
                    Speedyboarding = (bool)r.IdTransportNavigation.IdOptionTransportNavigation.Speedyboarding,
                    Prix = r.IdTransportNavigation.IdPrixTransportNavigation.Prix,
                    LieuDepart = r.IdTransportNavigation.LieuDepart,
                    CategorieTransportNom = r.IdTransportNavigation.IdCategorieTransportNavigation.Nom
                } : null,
                Voyage = r.IdVoyageNavigation != null ? new VoyageDTO
                {
                    IdVoyage = r.IdVoyageNavigation.IdVoyage,
                    DateDebut = r.IdVoyageNavigation.DateDebut,
                    DateFin = r.IdVoyageNavigation.DateFin,
                    IdClient = r.IdVoyageNavigation.IdClient ?? 0,
                    PrixTotal = r.IdVoyageNavigation.PrixTotal,
                    //IdPays = r.IdVoyageNavigation.IdPays.FirstOrDefault().IdPays, // Obtenez le premier IdPays
                } : null
            })
            .ToListAsync();

        return Ok(reservations);
    }
}
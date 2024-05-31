using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;

namespace projet_csharp_travel_plan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly TravelPlanContext _context;

        public ReservationsController(TravelPlanContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.IdLogementNavigation)
                .Include(r => r.IdActiviteNavigation)
                .Include(r => r.IdTransportNavigation)
                .Select(r => new ReservationDTO
                {
                    IdReservation = r.IdReservation,
                    IdVoyage = r.IdVoyage,
                    IdLogement = r.IdLogement,
                    IdActivite = r.IdActivite,
                    IdTransport = r.IdTransport,
                    DateHeureDebut = r.DateHeureDebut,
                    DateHeureFin = r.DateHeureFin,
                    Logement = r.IdLogementNavigation != null ? new LogementDTO
                    {
                        IdLogement = r.IdLogementNavigation.IdLogement,
                        Nom = r.IdLogementNavigation.Nom,
                        Details = r.IdLogementNavigation.Details,
                        Note = r.IdLogementNavigation.Note,
                        NbEvaluation = r.IdLogementNavigation.NbEvaluation,
                        Fournisseur = new FournisseurDTO
                        {
                            NomCompagnie = r.IdLogementNavigation.IdFournisseurNavigation.NomCompagnie
                        },
                        LogementCategorie = new LogementCategorieDTO
                        {
                            Nom = r.IdLogementNavigation.IdLogementCategorieNavigation.Nom
                        },
                        Pays = new PayDTO
                        {
                            Nom = r.IdLogementNavigation.IdPaysNavigation.Nom
                        }
                    } : null,
                    Activite = r.IdActiviteNavigation != null ? new ActiviteDTO
                    {
                        IdActivite = r.IdActiviteNavigation.IdActivite,
                        Nom = r.IdActiviteNavigation.Nom,
                        Details = r.IdActiviteNavigation.Details,
                        Note = r.IdActiviteNavigation.Note,
                        NbEvaluation = r.IdActiviteNavigation.NbEvaluation,
                        CategorieActiviteNom = r.IdActiviteNavigation.IdCatActivNavigation != null ? r.IdActiviteNavigation.IdCatActivNavigation.Nom : null,
                        FournisseurNom = r.IdActiviteNavigation.IdFournisseurNavigation != null ? r.IdActiviteNavigation.IdFournisseurNavigation.NomCompagnie : null,
                        PaysNom = r.IdActiviteNavigation.IdPaysNavigation != null ? r.IdActiviteNavigation.IdPaysNavigation.Nom : null,
                        PrixActivite = r.IdActiviteNavigation.IdPrixActiviteNavigation != null ? r.IdActiviteNavigation.IdPrixActiviteNavigation.Prix : (decimal?)null,
                        GuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.GuideAudio : (bool?)null,
                        PrixGuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixGuideAudio : (int?)null,
                        VisiteGuidee = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.VisiteGuidee : (bool?)null,
                        PrixVisiteGuide = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixVisiteGuide : (int?)null
                    } : null,
                    Transport = r.IdTransportNavigation != null ? new TransportDTO
                    {
                        IdTransport = r.IdTransportNavigation.IdTransport,
                        LieuDepart = r.IdTransportNavigation.LieuDepart,
                        HeureDepart = r.IdTransportNavigation.HeureDepart,
                        HeureArrivee = r.IdTransportNavigation.HeureArrivee,
                        Classe = r.IdTransportNavigation.Classe,
                        CategorieTransport = new TransportCategorieDTO
                        {
                            Nom = r.IdTransportNavigation.IdCategorieTransportNavigation.Nom
                        },
                        Fournisseur = new FournisseurDTO
                        {
                            NomCompagnie = r.IdTransportNavigation.IdFournisseurNavigation.NomCompagnie
                        },
                        VehiculeLoc = new VehiculeLocationDTO
                        {
                            Marque = r.IdTransportNavigation.IdVehiculeLocNavigation.Marque,
                            TypeVehicule = r.IdTransportNavigation.IdVehiculeLocNavigation.TypeVehicule,
                            NbSiege = r.IdTransportNavigation.IdVehiculeLocNavigation.NbSiege
                        },
                        PrixTransport = new TransportPrixDTO
                        {
                            Prix = r.IdTransportNavigation.IdPrixTransportNavigation.Prix
                        }
                    } : null
                })
                .ToListAsync();

            return reservations;
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.IdReservation)
            {
                return BadRequest();
            }

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

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.IdReservation }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.IdReservation == id);
        }
    }
}

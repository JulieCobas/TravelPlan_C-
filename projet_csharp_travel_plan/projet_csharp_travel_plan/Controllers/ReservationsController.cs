using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            #pragma warning disable CS8601 // Possible null reference assignment.
            var reservations = await _context.Reservations
                .Include(r => r.IdLogementNavigation)
                .Include(r => r.IdActiviteNavigation)
                .Include(r => r.IdTransportNavigation)
                .Select(r => new ReservationDTO
                {
                    Id = r.IdReservation,
                    IdVoyage = r.IdVoyage,
                    IdLogement = r.IdLogement,
                    IdActivite = r.IdActivite,
                    IdTransport = r.IdTransport,
                    DateHeureDebut = r.DateHeureDebut,
                    DateHeureFin = r.DateHeureFin,
                    Logement = r.IdLogementNavigation != null ? new LogementDTO
                    {
                        Id = r.IdLogementNavigation.IdLogement,
                        Nom = r.IdLogementNavigation.Nom,
                        Details = r.IdLogementNavigation.Details,
                        Note = r.IdLogementNavigation.Note,
                        NbEvaluation = r.IdLogementNavigation.NbEvaluation,
                        NomFournisseur = r.IdLogementNavigation.IdFournisseurNavigation.NomCompagnie,
                        NomCategorie = r.IdLogementNavigation.IdLogementCategorieNavigation.Nom,
                        NomPays = r.IdLogementNavigation.IdPaysNavigation.Nom
                    } : null,
                    Activite = r.IdActiviteNavigation != null ? new ActiviteDTO
                    {
                        IdActivite = r.IdActiviteNavigation.IdActivite,
                        IdOptionActivite = r.IdActiviteNavigation.IdOptionActivite,
                        IdPrixActivite = r.IdActiviteNavigation.IdPrixActivite,
                        IdPays = r.IdActiviteNavigation.IdPays,
                        IdFournisseur = r.IdActiviteNavigation.IdFournisseur,
                        IdCatActiv = r.IdActiviteNavigation.IdCatActiv,
                        Nom = r.IdActiviteNavigation.Nom,
                        Details = r.IdActiviteNavigation.Details,
                        Note = r.IdActiviteNavigation.Note,
                        NbEvaluation = r.IdActiviteNavigation.NbEvaluation,
                        HeuresMoyennes = r.IdActiviteNavigation.HeuresMoyennes,
                        Img = r.IdActiviteNavigation.Img,
                        CapaciteMax = r.IdActiviteNavigation.CapaciteMax,

                        // Navigation properties
                        CategorieActiviteNom = r.IdActiviteNavigation.IdCatActivNavigation != null ? r.IdActiviteNavigation.IdCatActivNavigation.Nom : null,
                        FournisseurNom = r.IdActiviteNavigation.IdFournisseurNavigation != null ? r.IdActiviteNavigation.IdFournisseurNavigation.NomCompagnie : null,
                        PaysNom = r.IdActiviteNavigation.IdPaysNavigation != null ? r.IdActiviteNavigation.IdPaysNavigation.Nom : null,
                        PrixActivite = r.IdActiviteNavigation.IdPrixActiviteNavigation != null ? r.IdActiviteNavigation.IdPrixActiviteNavigation.Prix : (decimal?)null,

                        // Option properties
                        GuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.GuideAudio : (bool?)null,
                        PrixGuideAudio = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixGuideAudio : (int?)null,
                        VisiteGuidee = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.VisiteGuidee : (bool?)null,
                        PrixVisiteGuide = r.IdActiviteNavigation.IdOptionActiviteNavigation != null ? r.IdActiviteNavigation.IdOptionActiviteNavigation.PrixVisiteGuide : (int?)null
                    } : null,
            Transport = r.IdTransportNavigation != null ? new TransportDto
                    {
                        IdTransport = r.IdTransportNavigation.IdTransport,
                        IdVehiculeLoc = r.IdTransportNavigation.IdVehiculeLoc,
                        IdCategorieTransport = r.IdTransportNavigation.IdCategorieTransport,
                        IdPrixTransport = r.IdTransportNavigation.IdPrixTransport,
                        IdOptionTransport = r.IdTransportNavigation.IdOptionTransport,
                        IdFournisseur = r.IdTransportNavigation.IdFournisseur,
                        IdPays = r.IdTransportNavigation.IdPays,
                        LieuDepart = r.IdTransportNavigation.LieuDepart,
                        HeureDepart = r.IdTransportNavigation.HeureDepart,
                        HeureArrivee = r.IdTransportNavigation.HeureArrivee,
                        Classe = r.IdTransportNavigation.Classe,
                        CategorieTransportNom = r.IdTransportNavigation.IdCategorieTransportNavigation.Nom,
                        FournisseurNomCompagnie = r.IdTransportNavigation.IdFournisseurNavigation.NomCompagnie,
                        VehiculeLocMarque = r.IdTransportNavigation.IdVehiculeLocNavigation.Marque,
                        VehiculeLocTypeVehicule = r.IdTransportNavigation.IdVehiculeLocNavigation.TypeVehicule,
                        VehiculeLocNbSiege = r.IdTransportNavigation.IdVehiculeLocNavigation.NbSiege,
                        PrixTransportPrix = r.IdTransportNavigation.IdPrixTransportNavigation.Prix
                    } : null
                })
                .ToListAsync();
#pragma warning restore CS8601 // Possible null reference assignment.

            return reservations;
        }



        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

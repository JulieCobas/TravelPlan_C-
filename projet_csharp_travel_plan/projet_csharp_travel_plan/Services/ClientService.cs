using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;
using projet_csharp_travel_plan.Models;
using System.Threading.Tasks;

namespace projet_csharp_travel_plan.Services
{
    public class ClientService : IClientService
    {
        private readonly TravelPlanNewDbContext _context;

        public ClientService(TravelPlanNewDbContext context)
        {
            _context = context;
        }

        public async Task<ClientDTO> GetClientAsync(string userId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == userId);
            if (client == null)
            {
                return null;
            }

            return new ClientDTO
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
        }

        public async Task UpdateClientAsync(ClientDTO clientDto)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == clientDto.IdClient);
            if (client == null)
            {
                throw new Exception("Client not found");
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

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}

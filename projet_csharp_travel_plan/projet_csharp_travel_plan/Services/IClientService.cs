using projet_csharp_travel_plan.DTO;

namespace projet_csharp_travel_plan.Services
{
    public interface IClientService
    {
        Task<ClientDTO> GetClientAsync(string userId);
        Task UpdateClientAsync(ClientDTO client);
    }
}

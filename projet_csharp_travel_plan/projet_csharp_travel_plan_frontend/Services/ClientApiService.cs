using projet_csharp_travel_plan_frontend.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace projet_csharp_travel_plan_frontend.Services
{
    public class ClientApiService
    {
        private readonly HttpClient _httpClient;

        public ClientApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClientDTO> GetClientAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"api/Clients/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClientDTO>();
        }

        public async Task UpdateClientAsync(ClientDTO clientDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Clients/{clientDto.IdClient}", clientDto);
            response.EnsureSuccessStatusCode();
        }
    }
}

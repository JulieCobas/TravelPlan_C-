using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace projet_csharp_travel_plan_frontend.Authentication
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public ApiKeyHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKey = _configuration.GetValue<string>("ApiSettings:ApiKey");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new HttpRequestException("API Key missing");
            }

            request.Headers.Add("X-Api-Key", apiKey);
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new HttpRequestException("Invalid API Key");
            }

            return response;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projet_csharp_travel_plan.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        public readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
                if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
                {
                    
                    context.Result = new UnauthorizedObjectResult("API Key missing");
                    return;
                }
                var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
                if (!apiKey.Equals(extractedApiKey))
                {
                context.Result = new UnauthorizedObjectResult("Invalid API kley");
                return;
                }
            }
    }
}

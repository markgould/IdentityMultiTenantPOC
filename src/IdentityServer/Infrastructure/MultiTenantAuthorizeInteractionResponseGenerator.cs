using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Infrastructure
{
    public class MultiTenantAuthorizeInteractionResponseGenerator : AuthorizeInteractionResponseGenerator
    {
        public MultiTenantAuthorizeInteractionResponseGenerator(ISystemClock clock, ILogger<AuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile)
            : base(clock, logger, consent, profile)
        {
        }

        public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        {
            var response = await base.ProcessInteractionAsync(request, consent);
            if (response.IsConsent || response.IsLogin || response.IsError)
                return response;

            if (request.Subject.HasClaim(c => c.Type == "tenantId"))
                return response;

            return new InteractionResponse
            {
                RedirectUrl = "/Tenant"
            };
        }
    }
}

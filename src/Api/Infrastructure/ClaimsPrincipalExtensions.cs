using System.Linq;
using System.Security.Claims;

namespace Api.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string[] GetAuthorizedTenants(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims
                .Where(c => c.Type == "authorizedTenants")
                .Select(c => c.Value)
                .ToArray();
        }

        public static bool IsAuthorizedForTenant(this ClaimsPrincipal claimsPrincipal, string tenantId)
        {
            return claimsPrincipal.GetAuthorizedTenants().Contains(tenantId);
        }
    }
}

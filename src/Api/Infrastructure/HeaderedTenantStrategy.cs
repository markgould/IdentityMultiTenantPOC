using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;

namespace Api.Infrastructure
{
    public class HeaderedTenantStrategy : IMultiTenantStrategy
    {
        public Task<string> GetIdentifierAsync(object context)
        {
            var httpContext = ((HttpContext)context);

            if (httpContext?.User == null || !httpContext.User.Identity.IsAuthenticated)
                return Task.FromResult(string.Empty);

            if (!httpContext.Request.Headers.TryGetValue("Tenant", out var tenantId))
                return Task.FromResult(string.Empty);

            var user = httpContext.User;
            return Task.FromResult(user.IsAuthorizedForTenant(tenantId) ? tenantId.ToString() : string.Empty);
        }
    }
}

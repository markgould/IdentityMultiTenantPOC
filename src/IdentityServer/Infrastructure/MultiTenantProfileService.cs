using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Infrastructure
{
    public class MultiTenantProfileService<TUser> : ProfileService<TUser>
        where TUser : class
    {
        public MultiTenantProfileService(UserManager<TUser> userManager, IUserClaimsPrincipalFactory<TUser> claimsFactory) : base(userManager, claimsFactory)
        {
        }

        public MultiTenantProfileService(UserManager<TUser> userManager, IUserClaimsPrincipalFactory<TUser> claimsFactory, ILogger<ProfileService<TUser>> logger) : base(userManager, claimsFactory, logger)
        {
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);

            var tenantClaim = context.Subject.FindFirst("tenantId");
            if (tenantClaim == null)
                return;

            context.IssuedClaims.Add(tenantClaim);
        }
    }
}

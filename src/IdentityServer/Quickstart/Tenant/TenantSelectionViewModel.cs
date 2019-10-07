using System.Collections.Generic;

namespace IdentityServer.Quickstart.Tenant
{
    public class TenantSelectionViewModel
    {
        public string ReturnUrl { get; set; }
        public IEnumerable<TenantViewModel> Tenants { get; set; }
    }
}

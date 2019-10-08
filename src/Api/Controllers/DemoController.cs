using System.Linq;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("demo")]
    [Authorize]
    public class DemoController : ControllerBase
    {
        private readonly IMultiTenantContextAccessor _tenantContextAccessor;

        public class Result
        {
            public string TenantId { get; set; }
            public string TenantName { get; set; }
        }

        public DemoController(IMultiTenantContextAccessor tenantContextAccessor)
        {
            _tenantContextAccessor = tenantContextAccessor;
        }

        public IActionResult Get()
        {
            var tenant = _tenantContextAccessor.MultiTenantContext.TenantInfo;

            if (tenant == null)
                return Unauthorized();

            var result = new Result { TenantId = tenant.Identifier, TenantName = tenant.Name };
            return new JsonResult(result);
        }
    }
}
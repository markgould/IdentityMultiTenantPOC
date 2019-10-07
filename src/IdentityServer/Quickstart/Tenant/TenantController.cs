using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Quickstart.Tenant
{
    [SecurityHeaders]
    [Authorize]
    public class TenantController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger<TenantController> _logger;

        public TenantController(IIdentityServerInteractionService interaction,
            ILogger<TenantController> logger)
        {
            _interaction = interaction;
            _logger = logger;
        }

        /// <summary>
        /// Shows the tenant screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await BuildViewModelAsync(returnUrl);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        /// <summary>
        /// Handles the tenant screen postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(TenantInputViewModel model)
        {
            await HttpContext.SignInAsync(User.Claims.Single(r => r.Type == "sub").Value,
                new System.Security.Claims.Claim("tenantId", model.SelectedTenant));
            return Redirect(model.ReturnUrl);
        }

        private async Task<TenantSelectionViewModel> BuildViewModelAsync(string returnUrl)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
            {
                _logger.LogError("No tenant request matching request: {0}", returnUrl);
                return null;
            }
            var tenants = new List<TenantViewModel>
            {
                new TenantViewModel { Id = "1", Name = "Tenant 1" },
                new TenantViewModel { Id = "2", Name = "Tenant 2" },
            };
            var vm = new TenantSelectionViewModel
            {
                Tenants = tenants,
                ReturnUrl = returnUrl
            };
            return vm;
        }
    }
}

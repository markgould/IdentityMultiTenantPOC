using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace MvcClient.Pages
{
    [Authorize]
    public class ApiDemoModel : PageModel
    {
        [BindProperty]
        public Command Data { get; set; }

        public Task OnGetAsync()
        {
            Data = new Command();
            return Task.CompletedTask;
        }

        public async Task OnPostAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync($"https://localhost:44325/demo?tenantId={Data.TenantId}");

            Data.Results = JToken.Parse(content).ToString();
        }

        public class Command
        {
            public string TenantId { get; set; }
            public string Results { get; set; }
        }

    }
}
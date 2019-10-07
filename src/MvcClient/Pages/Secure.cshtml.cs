using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace MvcClient.Pages
{
    [Authorize]
    public class SecureModel : PageModel
    {


        public void OnGet()
        {
        }

        public async Task OnCallApiAsync()
        {
  
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MvcClient.Pages
{
    [Authorize]
    public class SecureModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

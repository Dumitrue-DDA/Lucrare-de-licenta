using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Admin.Financials
{
    [Authorize(Roles = "admin, fin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

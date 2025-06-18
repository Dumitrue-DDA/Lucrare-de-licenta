using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Admin
{
    [Authorize(Policy = "EsteAngajat")]
    public class _AdminNavPartialModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Admin
{
    [Authorize(Policy = "EsteAngajat")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

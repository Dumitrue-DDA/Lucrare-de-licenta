using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Admin.Tours.Oferte
{
    [Authorize(Roles = "admin, spec_dez")]
    public class EditModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

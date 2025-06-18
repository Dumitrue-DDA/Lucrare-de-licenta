using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Admin.Tours.Tururi
{
    public class EditModel : PageModel
    {
        [Authorize(Roles = "admin, spec_dez")]
        public void OnGet()
        {
        }
    }
}

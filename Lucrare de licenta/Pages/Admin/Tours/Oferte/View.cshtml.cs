using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Admin.Tours.Oferte
{
    [Authorize(Roles = "admin, man_soft, ing_soft, man_op, spec_dez")]
    public class ViewModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

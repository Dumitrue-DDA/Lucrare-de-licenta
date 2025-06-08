using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Identity.Account.Manage
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                string? returnUrl = Url.Page("/Login");
                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = "/";
                }
                return RedirectToPage("/Identity/Account", new { ReturnUrl = returnUrl });
            }

            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Plecare { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Destinatie { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int nr_pers { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public DateOnly? data { get; set; }


        public void OnGet()
        {

        }
    }
}

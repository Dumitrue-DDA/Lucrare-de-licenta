using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages
{
    public class SearchModel : PageModel
    {
        public List<Tara> tarilist { get; set; }
        public void OnGet()
        {

        }
    }
}

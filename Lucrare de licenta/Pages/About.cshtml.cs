using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages
{
    public class SectionLink
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class AboutModel : PageModel
    {
        public string PageTitle { get; set; } = "About Us";

        public List<SectionLink> ContentSections { get; private set; }

        public void OnGet()
        {
            ContentSections = new List<SectionLink>
            {
                new SectionLink { Id="introducerea", Title="Introducere"}
            };
        }
    }
}

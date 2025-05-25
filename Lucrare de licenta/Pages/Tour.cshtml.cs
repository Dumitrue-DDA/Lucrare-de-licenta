using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages
{
    public class TourModel : PageModel
    {
        private readonly AppDbContext _context;
        public TourModel(AppDbContext context)
        {
            _context = context;
        }
        public TourViewModel TourInfo { get; set; }
        public class TourViewModel
        {

            public List<Itinerariu> Itinerarii { get; set; } = new List<Itinerariu>();
            public List<Oferta> Oferte { get; set; } = new List<Oferta>();
            public List<Categorie> Categorii { get; set; } = new List<Categorie>();
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var Tour = await _context.tururi
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.cod_tur == id.Value);
            return Page();
        }
    }
}

using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Admin.Bookings
{
    [Authorize(Roles = "admin, man_op")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Rezervare> Rezervari { get; set; } = new List<Rezervare>();

        public async Task OnGetAsync()
        {
            Rezervari = await _context.rezervari
                .Include(r => r.Oferta)
                    .ThenInclude(o => o.Tur)
                .Include(r => r.Utilizator)
                .ToListAsync();
        }
    }
}

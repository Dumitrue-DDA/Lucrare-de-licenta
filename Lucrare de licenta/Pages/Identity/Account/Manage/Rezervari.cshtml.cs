using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Identity.Account.Manage
{
    public class RezervariModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Utilizator> _userManager;
        public RezervariModel(AppDbContext context, UserManager<Utilizator> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Utilizator User { get; set; }
        public int UserId { get; set; }
        public IList<Rezervare> Rezervari { get; set; } = new List<Rezervare>();

        public async Task OnGetAsync()
        {
            UserId = (int)_userManager.GetUserId(User);
            Rezervari = await _context.rezervari
                .Where(r => r.nr_utilizator == UserId)
                .Include(r => r.Oferta)
                    .ThenInclude(o => o.Tur)
                .Include(r => r.Utilizator)
                .ToListAsync();
        }
    }
}

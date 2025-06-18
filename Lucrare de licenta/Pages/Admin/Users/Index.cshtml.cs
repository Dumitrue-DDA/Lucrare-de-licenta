using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Admin.Clients
{
    [Authorize(Roles = "admin, man_soft, ing_soft")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Utilizator> Utilizatori { get; private set; }
        public async Task OnGetAsync()
        {
            Utilizatori = await _context.utilizatori.ToListAsync();
        }
    }
}

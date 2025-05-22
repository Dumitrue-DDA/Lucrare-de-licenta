using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly AppDbContext _context;

        public UsersModel(AppDbContext context)
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

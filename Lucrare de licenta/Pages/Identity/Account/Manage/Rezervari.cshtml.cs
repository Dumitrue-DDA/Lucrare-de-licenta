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

        public Utilizator CurrentUser { get; set; }
        public int UserId { get; set; }
        public IList<Rezervare> Rezervari { get; set; } = new List<Rezervare>();
        public Dictionary<int, int> Count_beneficiari { get; set; } = new Dictionary<int, int>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            CurrentUser = user;

            var userIdString = await _userManager.GetUserIdAsync(user);
            UserId = int.Parse(userIdString);

            Rezervari = await _context.rezervari
                .Where(r => r.nr_utilizator == UserId)
                .Include(r => r.Oferta)
                .ThenInclude(o => o.Tur)
                .ToListAsync();

            var coduri_rezervari = Rezervari.Select(r => r.cod_rezervare).ToList();
            var camere = await _context.camere
                .Where(c => coduri_rezervari.Contains(c.cod_rezervare))
                .Select(c => new { c.cod_rezervare, c.cod_camera })
                .ToListAsync();
            var cod_camere = camere.Select(c => c.cod_camera).ToList();

            var cnt_beneficiari = await _context.beneficiari
                .Where(b => cod_camere.Contains(b.cod_camera))
                .GroupBy(b => b.cod_camera)
                .Select(g => new { cod_camera = g.Key, count = g.Count() })
                .ToListAsync();

            foreach (var rezervare in Rezervari)
            {
                Count_beneficiari[rezervare.cod_rezervare] = 0;
            }

            foreach (var cam in camere)
            {
                var b = cnt_beneficiari.FirstOrDefault(b => b.cod_camera == cam.cod_camera);

                Count_beneficiari[cam.cod_rezervare] += b.count;
            }
        }
    }
}

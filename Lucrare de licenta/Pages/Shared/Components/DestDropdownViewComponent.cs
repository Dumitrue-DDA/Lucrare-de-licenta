using Adventour.Data;
using Lucrare_de_licenta.Pages.Shared.Components.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Shared.Components
{
    public class DestDropdownViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public DestDropdownViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinatii = await _context.destinatii
                .OrderBy(d => d.cod_destinatie)
                .Include(d => d.Tara)
                .Take(10)
                .ToListAsync();
            var tari = await _context.tari
                .OrderBy(t => t.cod_tara)
                .Take(10)
                .ToListAsync();
            var ViewModel = new DestTarModel
            {
                Destinatii = destinatii,
                Tari = tari
            };
            return View(ViewModel);
        }
    }
}

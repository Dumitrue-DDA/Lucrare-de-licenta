using Adventour.Data;
using Lucrare_de_licenta.Pages.Shared.Components.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Shared.Components
{
    public class TurDropdownViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public TurDropdownViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tururi = await _context.tururi
                .OrderBy(t => t.den_tur)
                .Take(10)
                .ToListAsync();
            var categorii = await _context.categorii
                .OrderBy(c => c.den_categ)
                .Take(10)
                .ToListAsync();
            var ViewModel = new TurCategModel
            {
                Tururi = tururi,
                Categorii = categorii
            };
            return View(ViewModel);
        }
    }
}

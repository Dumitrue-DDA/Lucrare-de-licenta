using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Admin.Tours.Oferte
{
    [Authorize(Roles = "admin, spec_dez")]
    public class AddModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Oferta Oferta { get; set; } = new Oferta();

        public class turViewModel
        {
            public int cod_tur { get; set; }
            public string display { get; set; }
            public int nr_zile { get; set; }
        }
        public List<turViewModel> Tururi { get; set; } = new List<turViewModel>();

        public IList<Punct_Plecare> Puncte_Plecare { get; set; } = new List<Punct_Plecare>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Valori initiale
            Oferta.data_plecare = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            Oferta.data_intoarcere = DateOnly.FromDateTime(DateTime.Today.AddDays(8));
            Oferta.tip_transport = false;

            Puncte_Plecare = await _context.puncte_plecare
                .OrderBy(p => p.tip_transport)
                .ThenBy(p => p.localitate)
                .ToListAsync();

            Tururi = await _context.tururi
                .OrderBy(t => t.den_tur)
                .Select(t => new turViewModel
                {
                    cod_tur = t.cod_tur,
                    display = $"{t.den_tur}",
                    nr_zile = _context.itinerarii
                    .Where(i => i.cod_tur == t.cod_tur)
                    .Select(i => i.zi_activitate)
                    .Max()
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reincarcarea datelor de referenta
                await LoadReferenceData();
                return Page();
            }

            // preluam datele turului selectat
            var tour = await _context.tururi
                .Where(t => t.cod_tur == Oferta.cod_tur)
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                ModelState.AddModelError("Oferta.cod_tur", "Turul selectat nu exista");
                await LoadReferenceData();
                return Page();
            }

            // Calcularea datei de intoarcere dupa nr de zile in tur
            var nrZile = await _context.itinerarii
                .Where(i => i.cod_tur == Oferta.cod_tur)
                .Select(i => i.zi_activitate)
                .MaxAsync();

            Oferta.data_intoarcere = Oferta.data_plecare.AddDays(nrZile);

            // Setam locurile libere ca marimea grupului maxim
            Oferta.loc_libere = tour.grup_maxim;

            _context.oferte.Add(Oferta);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostSelectTourAsync()
        {
            if (Oferta.cod_tur <= 0)
                return Page();

            // Resetare formular
            ModelState.Clear();

            // Preluare detalii tur
            var tour = await _context.tururi
                .Where(t => t.cod_tur == Oferta.cod_tur)
                .FirstOrDefaultAsync();

            if (tour != null)
            {
                Oferta.loc_libere = tour.grup_maxim;

                // Calcularea datei de intoarcere
                var nrZile = await _context.itinerarii
                    .Where(i => i.cod_tur == Oferta.cod_tur)
                    .Select(i => i.zi_activitate)
                    .MaxAsync();

                Oferta.data_intoarcere = Oferta.data_plecare.AddDays(nrZile);
            }

            await LoadReferenceData();
            return Page();
        }

        // Incarcarea datelor referentiale
        private async Task LoadReferenceData()
        {
            Puncte_Plecare = await _context.puncte_plecare
                .OrderBy(p => p.tip_transport)
                .ThenBy(p => p.localitate)
                .ToListAsync();

            Tururi = await _context.tururi
                .OrderBy(t => t.den_tur)
                .Select(t => new turViewModel
                {
                    cod_tur = t.cod_tur,
                    display = $"{t.den_tur}",
                    nr_zile = _context.itinerarii
                    .Where(i => i.cod_tur == t.cod_tur)
                    .Select(i => i.zi_activitate)
                    .Max()
                }).ToListAsync();
        }
    }
}

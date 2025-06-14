using Adventour.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Admin.Tours.Oferte
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Filtrare
        [BindProperty(SupportsGet = true)]
        public string FilterCod { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterTur { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDataStart { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDataEnd { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterStatus { get; set; }

        // Sortare
        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; } = "data_plecare";

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "asc";

        // Model de afișare pentru o ofertă
        public class OfertaViewModel
        {
            public int cod_oferta { get; set; }
            public int cod_tur { get; set; }
            public string den_tur { get; set; }
            public string punct_plecare { get; set; }
            public DateOnly data_plecare { get; set; }
            public DateOnly data_retur { get; set; }
            public decimal pret { get; set; }
            public int loc_libere { get; set; }
            public int grup_minim { get; set; }
            public int grup_maxim { get; set; }
            public int nr_rezervari { get; set; }
        }

        public IList<OfertaViewModel> Oferte { get; set; } = new List<OfertaViewModel>();

        public async Task OnGetAsync()
        {
            // Încarcă datele necesare
            var oferte = await _context.oferte.ToListAsync();
            var tururi = await _context.tururi.ToListAsync();
            var rezervari = await _context.rezervari.ToListAsync();
            var puncte_plecare = await _context.puncte_plecare.ToListAsync();

            // Construiește view models
            var viewModels = oferte.Select(o =>
            {
                var tur = tururi.FirstOrDefault(t => t.cod_tur == o.cod_tur);
                var nrRezervari = rezervari.Count(r => r.cod_oferta == o.cod_oferta);
                var locuriDisponibile = o.loc_libere - nrRezervari;
                var punct = puncte_plecare.FirstOrDefault(p => p.cod_punct == o.cod_punct);

                return new OfertaViewModel
                {
                    cod_oferta = o.cod_oferta,
                    cod_tur = o.cod_tur,
                    den_tur = tur?.den_tur ?? "Necunoscut",
                    punct_plecare = punct?.localitate ?? "Necunoscut",
                    data_plecare = o.data_plecare,
                    data_retur = o.data_intoarcere,
                    pret = o.pret_adult,
                    loc_libere = locuriDisponibile < 0 ? 0 : locuriDisponibile,
                    grup_minim = tur?.grup_minim ?? 0,
                    grup_maxim = tur?.grup_maxim ?? 0,
                    nr_rezervari = nrRezervari
                };
            }).ToList();

            // Aplică filtrele
            if (!string.IsNullOrEmpty(FilterCod) && int.TryParse(FilterCod, out int codFilter))
            {
                viewModels = viewModels.Where(o => o.cod_oferta == codFilter).ToList();
            }

            if (!string.IsNullOrEmpty(FilterTur))
            {
                viewModels = viewModels.Where(o => o.den_tur.Contains(FilterTur, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (FilterDataStart.HasValue)
            {
                var dateOnly = DateOnly.FromDateTime(FilterDataStart.Value);
                viewModels = viewModels.Where(o => o.data_plecare >= dateOnly).ToList();
            }

            if (FilterDataEnd.HasValue)
            {
                var dateOnly = DateOnly.FromDateTime(FilterDataEnd.Value);
                viewModels = viewModels.Where(o => o.data_plecare <= dateOnly).ToList();
            }

            if (!string.IsNullOrEmpty(FilterStatus))
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                switch (FilterStatus.ToLower())
                {
                    case "in curs":
                        viewModels = viewModels.Where(o => o.data_plecare <= today && o.data_retur >= today).ToList();
                        break;
                    case "expirata":
                        viewModels = viewModels.Where(o => o.data_retur < today).ToList();
                        break;
                    case "viitoare":
                        viewModels = viewModels.Where(o => o.data_plecare > today).ToList();
                        break;
                }
            }

            // Aplică sortarea
            viewModels = SortColumn?.ToLower() switch
            {
                "cod_oferta" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.cod_oferta).ToList()
                    : viewModels.OrderBy(o => o.cod_oferta).ToList(),

                "tur" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.den_tur).ToList()
                    : viewModels.OrderBy(o => o.den_tur).ToList(),

                "data_plecare" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.data_plecare).ToList()
                    : viewModels.OrderBy(o => o.data_plecare).ToList(),

                "data_retur" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.data_retur).ToList()
                    : viewModels.OrderBy(o => o.data_retur).ToList(),

                "pret" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.pret).ToList()
                    : viewModels.OrderBy(o => o.pret).ToList(),

                "locuri" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.loc_libere).ToList()
                    : viewModels.OrderBy(o => o.loc_libere).ToList(),

                "rezervari" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.nr_rezervari).ToList()
                    : viewModels.OrderBy(o => o.nr_rezervari).ToList(),

                "status" => SortOrder?.ToLower() == "desc"
                    ? viewModels.OrderByDescending(o => o.data_plecare).ToList()
                    : viewModels.OrderBy(o => o.data_plecare).ToList(),

                _ => viewModels.OrderBy(o => o.data_plecare).ToList()
            };

            Oferte = viewModels;
        }
    }
}

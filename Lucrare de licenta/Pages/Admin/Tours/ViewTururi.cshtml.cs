using Adventour.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages.Admin.Tours
{
    public class ViewTururiModel : PageModel
    {
        public AppDbContext _context;

        public ViewTururiModel(AppDbContext context)
        {
            _context = context;
        }

        // Filtrarea afisarilor
        [BindProperty(SupportsGet = true)]
        public string FilterCod { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterDenumire { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterSolFizica { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterTara { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterOferte
        {
            get; set;
        }

        // modelul de view pentru un tur
        public class TurViewModel
        {
            public int cod_tur { get; set; }
            public string den_tur { get; set; }
            public int sol_fiz { get; set; }
            public int grup_minim { get; set; }
            public int grup_maxim { get; set; }
            public int zile { get; set; }
            public List<string> categorii { get; set; } = new List<string>();
            public List<string> tari { get; set; } = new List<string>();
            public List<string> destinatii { get; set; } = new List<string>();
            public int nr_oferte { get; set; }
            public int nr_rezervari { get; set; }

        }
        public IList<TurViewModel> Tururi { get; set; }
        public async Task OnGetAsync()
        {
            // Preluam toate rezultatele si le filtram in memorie
            var tours = await _context.tururi.ToListAsync();
            var itineraries = await _context.itinerarii.ToListAsync();
            var destItin = await _context.destinatii_itinerarii.ToListAsync();
            var destinations = await _context.destinatii.ToListAsync();
            var countries = await _context.tari.ToListAsync();
            var offers = await _context.oferte.ToListAsync();
            var turCategories = await _context.tur_categorii.ToListAsync();
            var categories = await _context.categorii.ToListAsync();
            var reservations = await _context.rezervari.ToListAsync();

            // Procesam datele
            var viewModels = tours.Select(tur =>
            {
                var tourItineraries = itineraries
                    .Where(i => i.cod_tur == tur.cod_tur)
                    .ToList();

                var maxDay = tourItineraries.Any() ?
                    tourItineraries.Max(i => i.zi_activitate) : 0;

                var destIds = new HashSet<short>();
                foreach (var itin in tourItineraries)
                {
                    var itinDestIds = destItin
                        .Where(di => di.cod_itinerariu == itin.cod_itinerariu)
                        .Select(di => di.cod_destinatie);

                    foreach (var id in itinDestIds)
                        destIds.Add(id);
                }

                var dests = destIds
                    .Select(id => destinations.FirstOrDefault(d => d.cod_destinatie == id))
                    .Where(d => d != null)
                    .ToList();

                var destNames = dests
                    .Select(d => d.den_destinatie)
                    .Where(n => !string.IsNullOrEmpty(n))
                    .Distinct()
                    .ToList();

                var countryIds = dests
                    .Select(d => d.cod_tara)
                    .Distinct()
                    .ToList();

                var countryNames = countries
                    .Where(c => countryIds.Contains(c.cod_tara))
                    .Select(c => c.den_tara)
                    .Where(n => !string.IsNullOrEmpty(n))
                    .Distinct()
                    .ToList();

                var catIds = turCategories
                    .Where(tc => tc.cod_tur == tur.cod_tur)
                    .Select(tc => tc.cod_categ)
                    .ToList();

                var categoryNames = categories
                    .Where(c => catIds.Contains(c.cod_categ))
                    .Select(c => c.den_categ)
                    .ToList();

                var tourOffers = offers
                    .Where(o => o.cod_tur == tur.cod_tur)
                    .ToList();

                var offerIds = tourOffers
                    .Select(o => o.cod_oferta)
                    .ToList();

                var tourReservations = reservations
                    .Count(r => offerIds.Contains(r.cod_oferta));

                return new TurViewModel
                {
                    cod_tur = tur.cod_tur,
                    den_tur = tur.den_tur,
                    sol_fiz = tur.sol_fiz,
                    grup_minim = tur.grup_minim,
                    grup_maxim = tur.grup_maxim,
                    zile = maxDay,
                    categorii = categoryNames,
                    tari = countryNames,
                    destinatii = destNames,
                    nr_oferte = tourOffers.Count,
                    nr_rezervari = tourReservations
                };
            }).ToList();

            // Apply filters
            var filteredViewModels = viewModels;

            if (!string.IsNullOrEmpty(FilterCod) && int.TryParse(FilterCod, out int codFilter))
            {
                filteredViewModels = filteredViewModels
                    .Where(t => t.cod_tur == codFilter)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(FilterDenumire))
            {
                filteredViewModels = filteredViewModels
                    .Where(t => t.den_tur.Contains(FilterDenumire, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(FilterSolFizica) && int.TryParse(FilterSolFizica, out int solFizFilter))
            {
                filteredViewModels = filteredViewModels
                    .Where(t => t.sol_fiz == solFizFilter)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(FilterTara))
            {
                filteredViewModels = filteredViewModels
                    .Where(t => t.tari.Any(tara => tara.Contains(FilterTara, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(FilterOferte))
            {
                if (FilterOferte == "yes")
                {
                    filteredViewModels = filteredViewModels
                        .Where(t => t.nr_oferte > 0)
                        .ToList();
                }
                else if (FilterOferte == "no")
                {
                    filteredViewModels = filteredViewModels
                        .Where(t => t.nr_oferte == 0)
                        .ToList();
                }
            }

            Tururi = filteredViewModels;
        }
    }
}

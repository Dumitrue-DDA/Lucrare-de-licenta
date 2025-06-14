using Adventour.Data;
using Lucrare_de_licenta.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages
{
    public class SearchModel : PageModel
    {
        public AppDbContext _context;
        public ILogger<SearchModel> _logger;

        public string Imagepath = "\\Resources\\";

        public SearchModel(AppDbContext context, ILogger<SearchModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Variabilele de cautare
        [BindProperty(SupportsGet = true)]
        public string Plecare { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Destinatie { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int nr_pers { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public DateOnly? data { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public int? continent { get; set; } = null;

        public string Truncate(string? text, int maxLen)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (text.Length <= maxLen)
            {
                return text;
            }

            return text.Substring(0, maxLen - 3) + "...";
        }
        // Entitatile de afisat
        public IList<TurCardViewModel> Tururi { get; set; } = new List<TurCardViewModel>();
        public async Task<IActionResult> OnGetAsync()
        {
            // construim interogarea 
            var query = from tur in _context.tururi


                        join oferta in _context.oferte on tur.cod_tur equals oferta.cod_tur
                        into oferteGroup
                        from oferta in oferteGroup.DefaultIfEmpty()

                        join itinerariu in _context.itinerarii on tur.cod_tur equals itinerariu.cod_tur
                        into itinerariuGroup
                        from itinerariu in itinerariuGroup.DefaultIfEmpty()

                        join dest_itin in _context.destinatii_itinerarii
                        on itinerariu.cod_itinerariu equals dest_itin.cod_itinerariu
                        into destItinGroup
                        from dest_itin in destItinGroup.DefaultIfEmpty()

                        join destinatie in _context.destinatii on dest_itin.cod_destinatie equals
                        destinatie.cod_destinatie into destGroup
                        from destinatie in destGroup.DefaultIfEmpty()

                        join tara in _context.tari on destinatie.cod_tara equals tara.cod_tara into tariGroup
                        from tara in tariGroup.DefaultIfEmpty()

                        group new { destinatie, tara, oferta, itinerariu } by new
                        {
                            tur.cod_tur,
                            tur.den_tur,
                            tur.desc_tur,
                            tur.img_tur
                        } into g

                        select new TurCardViewModel
                        {
                            cod_tur = g.Key.cod_tur,
                            den_tur = g.Key.den_tur,
                            desc_tur = g.Key.desc_tur,
                            img_tur = g.Key.img_tur,

                            tip_transport =
                            (g.Any(x => x.oferta != null && x.oferta.tip_transport == false) && g.Any(x => x.oferta != null && x.oferta.tip_transport == true)) ? 2 :
                            (g.Any(x => x.oferta != null && x.oferta.tip_transport == true)) ? 1 : 0,

                            zile = g.Where(x => x.itinerariu != null)
                                    .Select(x => x.itinerariu.zi_activitate)
                                    .Max(),

                            pret_min = g.Where(x => x.oferta != null && x.oferta.pret_adult > 0m)
                                          .Select(x => (decimal?)x.oferta.pret_adult)
                                          .Min(),

                            Destinatii = g.Where(x => x.destinatie != null)
                                            .Select(x => x.destinatie.den_destinatie)
                                            .Distinct()
                                            .ToList(),

                            Tari = g.Where(x => x.tara != null)
                                     .Select(x => x.tara.den_tara)
                                     .Distinct()
                                     .ToList(),
                        };

            Tururi = await query.ToListAsync();

            return Page();
        }
    }
}

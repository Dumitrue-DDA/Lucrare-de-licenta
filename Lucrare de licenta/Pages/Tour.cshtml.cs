using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lucrare_de_licenta.Pages
{
    public class TourModel : PageModel
    {
        private readonly AppDbContext _context;
        public TourModel(AppDbContext context)
        {
            _context = context;
        }
        public string Imagepath = "~\\Resources\\";

        public SelectList List_Puncte { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? id_Punct { get; set; }

        public Tur? Tur { get; set; }
        public IList<Tur_categorie>? Tururi_Categorii { get; set; } = new List<Tur_categorie>();
        public IList<Categorie>? Categorii { get; set; } = new List<Categorie>();
        public IList<Itinerariu>? Itinerarii { get; set; } = new List<Itinerariu>();
        public IList<Cazare>? Cazari { get; set; } = new List<Cazare>();
        public IList<Destinatie_itinerariu>? Destinatii_Itinerarii { get; set; } = new List<Destinatie_itinerariu>();
        public IList<Destinatie>? Destinatii { get; set; } = new List<Destinatie>();
        public IList<Tara>? Tari { get; set; } = new List<Tara>();
        public IList<Oferta>? Oferte { get; set; } = new List<Oferta>
        {
            new Oferta {
               cod_oferta= 1,
               tip_transport = true,
               pret_adult = 100m,
               pret_copil = 50m,
               data_plecare = new DateOnly(2025, 10, 10),
               data_intoarcere = new DateOnly(2025, 10, 15),
               loc_libere = 10,
               cod_tur = 1,
               cod_punct = 1,
               Punct = new Punct_Plecare { cod_punct = 1, localitate = "Bucuresti" }
            },
            new Oferta
            {
               cod_oferta= 2,
               tip_transport = false,
               pret_adult = 90m,
               pret_copil = 0,
               data_plecare = new DateOnly(2025, 11, 10),
               data_intoarcere = new DateOnly(2025, 11, 15),
               loc_libere = 0,
               cod_tur = 1,
               cod_punct = 2
            }
        };
        public IList<Punct_Plecare>? Puncte_Plecare { get; set; } = new List<Punct_Plecare>();

        public decimal? Pret_Min()
        {
            decimal pret = 0m;
            if (!Oferte.Any())
                return null;

            foreach (Oferta o in Oferte)
            {
                if (o.loc_libere > 0 && o.pret_adult > 0)
                {
                    if (pret == 0)
                    {
                        pret = o.pret_adult;
                    }
                    pret = Math.Min(pret, o.pret_adult);
                }
            }
            if (pret == 0m) return null;
            return pret;
        }
        /// <summary>
        /// Cauta destinatiile si tarile itinerariului cu codul dat.
        /// Returneaza o lista cu string-uri de format denumire_destinatie - denumire_tara
        /// </summary>
        /// <param name="cod_itin">Codul Itinerariului</param>
        /// <returns>Lista de string-uri</returns>
        public List<string> GetItinDestText(int cod_itin)
        {
            List<string> rezultat = new List<string>();

            // cautam tupluirle aferente codului in tabela de jonctiune
            var dest_ids = Destinatii_Itinerarii
                        .Where(di => di.cod_itinerariu == cod_itin)
                        .Select(di => di.cod_destinatie)
                        .ToList();

            // preluam denumirile si codul tarii pentru destinatiile gasite
            var dest = Destinatii
                .Where(d => dest_ids.Contains(d.cod_destinatie))
                .Select(d => new
                {
                    id_tara = d.cod_tara,
                    den = d.den_destinatie
                })
                .ToList();

            // cautam denumirile tarilor si inseram stringurile in lista de rezultate
            foreach (var d in dest)
            {
                var den_tara = Tari
                    .Where(t => t.cod_tara == d.id_tara)
                    .Select(t => t.den_tara)
                    .FirstOrDefault();
                rezultat.Add($"{d.den} - {den_tara}");
            }

            return rezultat;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Error");
            }

            // Cautam turul dupa codul sau
            Tur = await _context.tururi
                .FirstOrDefaultAsync(t => t.cod_tur == id);

            if (Tur == null)
            {
                return RedirectToPage("/Error");
            }

            // Initializam restul tabelelor necesare 
            Tururi_Categorii = await _context.tur_categorii
                .Where(tc => tc.cod_tur == id)
                .ToListAsync();

            var categoryIds = Tururi_Categorii.Select(tc => tc.cod_categ).ToList();
            Categorii = await _context.categorii
                .Where(c => categoryIds.Contains(c.cod_categ))
                .ToListAsync();

            Itinerarii = await _context.itinerarii
                .Where(i => i.cod_tur == id)
                .OrderBy(i => i.zi_activitate) // ordonate dupa zi pentru afisarea usoara
                .ToListAsync();

            var accommodationIds = Itinerarii
                .Where(i => i.cod_cazare.HasValue)
                .Select(i => i.cod_cazare.Value)
                .Distinct()
                .ToList();

            var itineraryIds = Itinerarii.Select(i => i.cod_itinerariu).ToList();

            if (accommodationIds.Any())
            {
                Cazari = await _context.cazari
                    .Where(c => accommodationIds.Contains(c.cod_cazare))
                    .ToListAsync();
            }

            Destinatii_Itinerarii = await _context.destinatii_itinerarii
                .Where(di => itineraryIds.Contains(di.cod_itinerariu))
                .ToListAsync();

            var destinationIds = Destinatii_Itinerarii.Select(di => di.cod_destinatie).Distinct().ToList();
            Destinatii = await _context.destinatii
                .Where(d => destinationIds.Contains(d.cod_destinatie))
                .ToListAsync();

            var countryIds = Destinatii.Select(d => d.cod_tara).Distinct().ToList();
            Tari = await _context.tari
                .Where(t => countryIds.Contains(t.cod_tara))
                .ToListAsync();
            /* TESTE
            Oferte = await _context.oferte
                .Where(o => o.cod_tur == id && o.loc_libere > 0)
                .Include("Punct")
                .ToListAsync();
            */
            var departurePointIds = Oferte.Select(o => o.cod_punct).Distinct().ToList();
            Puncte_Plecare = await _context.puncte_plecare
                .Where(pp => departurePointIds.Contains(pp.cod_punct))
                .ToListAsync();

            return Page();
        }
    }
}

using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public Tur? Tur { get; set; }
        public IList<Tur_categorie>? Tururi_Categorii { get; set; } = new List<Tur_categorie>();
        public IList<Categorie>? Categorii { get; set; } = new List<Categorie>();
        public IList<Itinerariu>? Itinerarii { get; set; } = new List<Itinerariu>();
        public IList<Cazare>? Cazari { get; set; } = new List<Cazare>();
        public IList<Destinatie_itinerariu>? Destinatii_Itinerarii { get; set; } = new List<Destinatie_itinerariu>();
        public IList<Destinatie>? Destinatii { get; set; } = new List<Destinatie>();
        public IList<Tara>? Tari { get; set; } = new List<Tara>();
        public IList<Oferta>? Oferte { get; set; } = new List<Oferta>();
        public IList<Punct_Plecare>? Puncte_Plecare { get; set; } = new List<Punct_Plecare>();
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Error");
            }

            // Load the tour with a single query
            Tur = await _context.tururi
                .FirstOrDefaultAsync(t => t.cod_tur == id);

            if (Tur == null)
            {
                return RedirectToPage("/Error");
            }

            // Load categories in one query
            Tururi_Categorii = await _context.tur_categorii
                .Where(tc => tc.cod_tur == id)
                .ToListAsync();

            var categoryIds = Tururi_Categorii.Select(tc => tc.cod_categ).ToList();
            Categorii = await _context.categorii
                .Where(c => categoryIds.Contains(c.cod_categ))
                .ToListAsync();

            // Load itineraries and related data in one query
            Itinerarii = await _context.itinerarii
                .Where(i => i.cod_tur == id)
                .ToListAsync();

            var accommodationIds = Itinerarii
                .Where(i => i.cod_cazare.HasValue)
                .Select(i => i.cod_cazare.Value)
                .Distinct()
                .ToList();

            var itineraryIds = Itinerarii.Select(i => i.cod_itinerariu).ToList();

            // Batch load accommodations
            if (accommodationIds.Any())
            {
                Cazari = await _context.cazari
                    .Where(c => accommodationIds.Contains(c.cod_cazare))
                    .ToListAsync();
            }

            // Batch load destination-itinerary mappings
            Destinatii_Itinerarii = await _context.destinatii_itinerarii
                .Where(di => itineraryIds.Contains(di.cod_itinerariu))
                .ToListAsync();

            // Extract destination IDs and load destinations
            var destinationIds = Destinatii_Itinerarii.Select(di => di.cod_destinatie).Distinct().ToList();
            Destinatii = await _context.destinatii
                .Where(d => destinationIds.Contains(d.cod_destinatie))
                .ToListAsync();

            // Extract country IDs and load countries
            var countryIds = Destinatii.Select(d => d.cod_tara).Distinct().ToList();
            Tari = await _context.tari
                .Where(t => countryIds.Contains(t.cod_tara))
                .ToListAsync();

            // Load offers and departure points
            Oferte = await _context.oferte
                .Where(o => o.cod_tur == id)
                .ToListAsync();

            var departurePointIds = Oferte.Select(o => o.cod_punct).Distinct().ToList();
            Puncte_Plecare = await _context.puncte_plecare
                .Where(pp => departurePointIds.Contains(pp.cod_punct))
                .ToListAsync();

            return Page();
        }
    }
}

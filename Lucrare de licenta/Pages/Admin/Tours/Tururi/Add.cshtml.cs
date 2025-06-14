using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lucrare_de_licenta.Pages.Admin.Tours.Tururi
{
    public class AddModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Tur Tur { get; set; } = new Tur();

        [BindProperty]
        public List<ItineraryViewModel> Itinerarii { get; set; } = new List<ItineraryViewModel>();

        [BindProperty]
        [Display(Name = "Imagine Tur")]
        public IFormFile ImageFile { get; set; }

        [BindProperty]
        public List<IFormFile> ItineraryImages { get; set; } = new List<IFormFile>();

        [BindProperty]
        public int[] SelectedDestinatii { get; set; }

        [BindProperty]
        public int[] SelectedCategorii { get; set; }

        public SelectList CazariList { get; set; }
        public SelectList DestinatiiList { get; set; }
        public SelectList CategoriiList { get; set; }

        public Dictionary<string, short> CazariDestinatii { get; set; } = new Dictionary<string, short>();

        public async Task OnGetAsync()
        {
            await LoadSelectLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists();
                return Page();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Salvăm imaginea turului
                if (ImageFile != null)
                {
                    var uniqueFileName = await SaveImage(ImageFile, "tours");
                    Tur.img_tur = uniqueFileName;
                }

                // Adăugăm turul
                _context.tururi.Add(Tur);
                await _context.SaveChangesAsync();

                // Adăugăm itinerariile
                for (int i = 0; i < Itinerarii.Count; i++)
                {
                    var itineraryViewModel = Itinerarii[i];
                    var itinerariu = itineraryViewModel.Itinerariu;
                    itinerariu.cod_tur = Tur.cod_tur;

                    // Salvăm imaginea pentru itinerariu
                    if (i < ItineraryImages.Count && ItineraryImages[i] != null)
                    {
                        var uniqueFileName = await SaveImage(ItineraryImages[i], "itineraries");
                        itinerariu.img_itin = uniqueFileName;
                    }

                    _context.itinerarii.Add(itinerariu);
                }
                await _context.SaveChangesAsync();

                // Procesăm destinațiile pentru fiecare itinerariu
                for (int i = 0; i < Itinerarii.Count; i++)
                {
                    var itinerariu = Itinerarii[i];

                    // Procesăm destinațiile selectate pentru acest itinerariu
                    var key = $"Itinerarii[{i}].SelectedDestinatii";
                    if (Request.Form.ContainsKey(key))
                    {
                        var selectedDestinatiiForItinerary = Request.Form[key]
                            .Select(int.Parse)
                            .ToArray();

                        foreach (var destId in selectedDestinatiiForItinerary)
                        {
                            var destItin = new Destinatie_itinerariu
                            {
                                cod_itinerariu = itinerariu.Itinerariu.cod_itinerariu,
                                cod_destinatie = (short)destId
                            };
                            _context.destinatii_itinerarii.Add(destItin);
                        }
                    }
                }
                await _context.SaveChangesAsync();

                // Adăugăm categoriile selectate
                if (SelectedCategorii != null)
                {
                    foreach (var catId in SelectedCategorii)
                    {
                        var turCat = new Tur_categorie
                        {
                            cod_tur = Tur.cod_tur,
                            cod_categ = (byte)catId
                        };
                        _context.tur_categorii.Add(turCat);
                    }
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "A apărut o eroare la salvarea turului: " + ex.Message);
                await LoadSelectLists();
                return Page();
            }
        }

        private async Task LoadSelectLists()
        {
            // Încărcăm listele pentru dropdown-uri
            CazariList = new SelectList(
                await _context.cazari
                    .OrderBy(c => c.den_cazare)
                    .Select(c => new { c.cod_cazare, Display = c.den_cazare })
                    .ToListAsync(),
                "cod_cazare", "Display");

            DestinatiiList = new SelectList(
                await _context.destinatii
                    .OrderBy(d => d.den_destinatie)
                    .Select(d => new { d.cod_destinatie, Display = d.den_destinatie })
                    .ToListAsync(),
                "cod_destinatie", "Display");

            CategoriiList = new SelectList(
                await _context.categorii
                    .OrderBy(c => c.den_categ)
                    .Select(c => new { c.cod_categ, Display = c.den_categ })
                    .ToListAsync(),
                "cod_categ", "Display");

            // Încărcăm destinațiile pentru fiecare cazare
            var cazariWithDestinatii = await _context.cazari
                .Select(c => new { c.cod_cazare, c.cod_destinatie })
                .ToListAsync();

            foreach (var cazare in cazariWithDestinatii)
            {
                CazariDestinatii[cazare.cod_cazare.ToString()] = cazare.cod_destinatie;
            }
        }

        private async Task<string> SaveImage(IFormFile imageFile, string subfolder)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", subfolder);

            // Asigură-te că folderul există
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generează un nume unic pentru fișier
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Salvează fișierul
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"/images/{subfolder}/{uniqueFileName}";
        }
    }

    public class ItineraryViewModel
    {
        public Itinerariu Itinerariu { get; set; } = new Itinerariu();
        public int[] SelectedDestinatii { get; set; }
    }
}
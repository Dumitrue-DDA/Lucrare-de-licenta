using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lucrare_de_licenta.Pages.Admin.Tours.Tururi
{
    [Authorize(Roles = "admin, spec_dez")]
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
        public List<Itin_ViewModel> Itinerarii { get; set; } = new List<Itin_ViewModel>();

        [BindProperty]
        [Display(Name = "Imagine Tur")]
        public IFormFile Img_Tur { get; set; }

        [BindProperty]
        public List<IFormFile> Img_Itinerarii { get; set; } = new List<IFormFile>();

        [BindProperty]
        public int[] Dest_Selectate { get; set; }

        [BindProperty]
        public int[] Categ_Selectate { get; set; }

        public SelectList Cazari_List { get; set; }
        public SelectList Destinatii_List { get; set; }
        public SelectList Categorii_List { get; set; }

        public Dictionary<string, short> Cazari_Destinatii { get; set; } = new Dictionary<string, short>();

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
                // Salvam imaginea turului
                if (Img_Tur != null)
                {
                    var uniqueFileName = await SaveImage(Img_Tur, "tours");
                    Tur.img_tur = uniqueFileName;
                }

                // Adaugam turul
                _context.tururi.Add(Tur);
                await _context.SaveChangesAsync();

                // Adaugam itinerariile
                for (int i = 0; i < Itinerarii.Count; i++)
                {
                    var itinerariu_ViewModel = Itinerarii[i];
                    var itinerariu = itinerariu_ViewModel.Itinerariu;
                    itinerariu.cod_tur = Tur.cod_tur;

                    // Salvam imaginea pentru itinerariu
                    if (i < Img_Itinerarii.Count && Img_Itinerarii[i] != null)
                    {
                        var nume_fisier = await SaveImage(Img_Itinerarii[i], "itineraries");
                        itinerariu.img_itin = nume_fisier;
                    }

                    _context.itinerarii.Add(itinerariu);
                }
                await _context.SaveChangesAsync();

                // Procesam destinatiile pentru fiecare itinerariu
                for (int i = 0; i < Itinerarii.Count; i++)
                {
                    var itinerariu = Itinerarii[i];

                    // Procesam destinatiile selectate pentru acest itinerariu
                    var key = $"Itinerarii[{i}].Dest_Selectate";
                    if (Request.Form.ContainsKey(key))
                    {
                        var Dest_Itin = Request.Form[key]
                            .Select(int.Parse)
                            .ToArray();

                        foreach (var cod_dest in Dest_Itin)
                        {
                            var dest_itin = new Destinatie_itinerariu
                            {
                                cod_itinerariu = itinerariu.Itinerariu.cod_itinerariu,
                                cod_destinatie = (short)cod_dest
                            };
                            _context.destinatii_itinerarii.Add(dest_itin);
                        }
                    }
                }
                await _context.SaveChangesAsync();

                // Adaugam categoriile selectate
                if (Categ_Selectate != null)
                {
                    foreach (var cod_categ in Categ_Selectate)
                    {
                        var tur_categ = new Tur_categorie
                        {
                            cod_tur = Tur.cod_tur,
                            cod_categ = (byte)cod_categ
                        };
                        _context.tur_categorii.Add(tur_categ);
                    }
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "A aparut o eroare la salvarea turului: " + ex.Message);
                await LoadSelectLists();
                return Page();
            }
        }

        private async Task LoadSelectLists()
        {
            // Incarcam listele pentru dropdown-uri
            Cazari_List = new SelectList(
                await _context.cazari
                    .OrderBy(c => c.den_cazare)
                    .Select(c => new { c.cod_cazare, Display = c.den_cazare })
                    .ToListAsync(),
                "cod_cazare", "Display");

            Destinatii_List = new SelectList(
                await _context.destinatii
                    .OrderBy(d => d.den_destinatie)
                    .Select(d => new { d.cod_destinatie, Display = d.den_destinatie })
                    .ToListAsync(),
                "cod_destinatie", "Display");

            Categorii_List = new SelectList(
                await _context.categorii
                    .OrderBy(c => c.den_categ)
                    .Select(c => new { c.cod_categ, Display = c.den_categ })
                    .ToListAsync(),
                "cod_categ", "Display");

            // Incarcam destinatiile pentru fiecare cazare
            var cazari_dest = await _context.cazari
                .Select(c => new { c.cod_cazare, c.cod_destinatie })
                .ToListAsync();

            foreach (var cazare in cazari_dest)
            {
                Cazari_Destinatii[cazare.cod_cazare.ToString()] = cazare.cod_destinatie;
            }
        }

        private async Task<string> SaveImage(IFormFile image_file, string subfolder)
        {
            var folder_path = Path.Combine(_environment.WebRootPath, "Resources", subfolder);

            // Asigura-te ca folderul exista
            if (!Directory.Exists(folder_path))
            {
                Directory.CreateDirectory(folder_path);
            }

            // Genereaza un nume unic pentru fisier
            var nume_fisier = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image_file.FileName);
            var file_path = Path.Combine(folder_path, nume_fisier);

            // Salveaza fisierul
            using (var fileStream = new FileStream(file_path, FileMode.Create))
            {
                await image_file.CopyToAsync(fileStream);
            }

            return $"/Resources/{subfolder}/{nume_fisier}";
        }
    }

    public class Itin_ViewModel
    {
        public Itinerariu Itinerariu { get; set; } = new Itinerariu();
        public int[] Dest_Selectate { get; set; }
    }
}
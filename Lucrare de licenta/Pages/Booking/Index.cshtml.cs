using Adventour.Data;
using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lucrare_de_licenta.Pages.Booking
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Utilizator> _userManager;

        public IndexModel(AppDbContext context, UserManager<Utilizator> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public int cod_oferta { get; set; }

        [DataType(DataType.Date)]
        public DateTime curr_date { get; set; }

        [BindProperty]
        public Rezervare_Form FormData { get; set; } = new Rezervare_Form();
        public Oferta Oferta { get; set; } = new Oferta();

        public IActionResult OnGet()
        {
            if (cod_oferta <= 0)
            {
                return RedirectToPage("/Error", new { message = "Ofertă invalidă" });
            }

            // Initializam cu o camera cu un beneficiar
            FormData.Camere.Add(new Camera_Form
            {
                Beneficiari = new List<Beneficiar_Form> { new Beneficiar_Form() }
            });

            Oferta = _context.oferte
                     .Include(o => o.Tur)
                     .Include(o => o.Punct)
                     .FirstOrDefault(o => o.cod_oferta == cod_oferta);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Calculul sumei totale de plata
            decimal sumaTotal = 0;
            var oferta = _context.oferte.FirstOrDefault(o => o.cod_oferta == cod_oferta);
            if (oferta == null)
            {
                ModelState.AddModelError(string.Empty, "Oferta nu a fost gasita.");
                return Page();
            }

            DateOnly data_intoarcere = oferta.data_intoarcere;
            int adult_cnt = 0;
            int copil_cnt = 0;

            foreach (var camera in FormData.Camere)
            {
                foreach (var beneficiar in camera.Beneficiari)
                {
                    // Calculam dupa varsta la data intoarcerii
                    int age = CalculateAge(beneficiar.Data_Nastere, data_intoarcere);

                    if (age >= 18)
                    {
                        adult_cnt++;
                    }
                    else
                    {
                        copil_cnt++;
                    }
                }
            }

            if ((oferta.pret_copil == 0 || oferta.pret_copil == null) && (copil_cnt > 0))
            {
                ModelState.AddModelError(string.Empty, "Oferta nu a fost gasita.");
                return Page();
            }

            sumaTotal = (adult_cnt * oferta.pret_adult) + (copil_cnt * oferta.pret_copil);

            // Preluam nuamrul de utilizator daca este logat
            int? nrUtilizator = null;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                nrUtilizator = user?.Id;
            }

            var rezervare = new Rezervare
            {
                data_rezervare = DateOnly.FromDateTime(DateTime.Now),
                email_contact = FormData.Email,
                tel_contact = FormData.Telefon,
                status_rezervare = 1, // Initial status
                cod_oferta = cod_oferta,
                nr_utilizator = nrUtilizator
            };

            _context.rezervari.Add(rezervare);
            await _context.SaveChangesAsync(); // Save to get the reservation ID

            // Create rooms
            foreach (var camera_form in FormData.Camere)
            {
                var camera = new Camera
                {
                    cod_rezervare = rezervare.cod_rezervare
                };

                _context.camere.Add(camera);
                await _context.SaveChangesAsync(); // Save to get the camera ID

                // Create beneficiaries for this room
                foreach (var beneficiar_form in camera_form.Beneficiari)
                {
                    var beneficiar = new Beneficiar
                    {
                        nume_beneficiar = beneficiar_form.Nume,
                        prenume_beneficiar = beneficiar_form.Prenume,
                        data_nastere = beneficiar_form.Data_Nastere,
                        cod_camera = camera.cod_camera
                    };

                    _context.beneficiari.Add(beneficiar);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Booking/Confirmation", new { id = rezervare.cod_rezervare });
        }
        private int CalculateAge(DateOnly birthDate, DateOnly referenceDate)
        {
            int age = referenceDate.Year - birthDate.Year;

            // Check if birthday has occurred this year
            if (birthDate.Month > referenceDate.Month ||
                (birthDate.Month == referenceDate.Month && birthDate.Day > referenceDate.Day))
            {
                age--;
            }

            return age;
        }
    }

    public class Rezervare_Form
    {
        [Required(ErrorMessage = "Emailul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Număr de telefon invalid")]
        public string Telefon { get; set; }

        public List<Camera_Form> Camere { get; set; } = new List<Camera_Form>();
    }

    public class Camera_Form
    {
        public List<Beneficiar_Form> Beneficiari { get; set; } = new List<Beneficiar_Form>();
    }

    public class Beneficiar_Form
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        public string Prenume { get; set; }

        [Required(ErrorMessage = "Data nașterii este obligatorie")]
        [DataType(DataType.Date)]
        public DateOnly Data_Nastere { get; set; }
    }
}

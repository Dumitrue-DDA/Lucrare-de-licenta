using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Lucrare_de_licenta.Pages.Identity.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<Utilizator> _userManager;
        private readonly SignInManager<Utilizator> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<Utilizator> userManager,
            SignInManager<Utilizator> signInManager,
        ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "parola curenta")]
            public string OldPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6)]
            [Display(Name = "parola noua")]
            public string NewPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "A-ti introdus o parola difertita de cea noua.")]
            [Display(Name = "Confirma parola noua")]
            public string ConfirmPassword { get; set; }
        }

        // In cazul unui utilizator care nu este logat, se va redirectiona catre pagina de login
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("../Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Nu am putut gasi utilizatorul");
            }

            var result = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("Utilizatorul si-a schimbat parola cu succes.");
            StatusMessage = "Parola a fost schimbata cu succes.";
            return RedirectToPage();
        }
    }
}

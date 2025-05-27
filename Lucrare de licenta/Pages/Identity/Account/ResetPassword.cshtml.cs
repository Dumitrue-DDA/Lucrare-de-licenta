using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lucrare_de_licenta.Pages.Identity.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<Utilizator> _userManager;
        private readonly ILogger<ResetPasswordModel> _logger;

        public ResetPasswordModel(
            UserManager<Utilizator> userManager,
            ILogger<ResetPasswordModel> logger
            )
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Confirm Password")]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Code { get; set; }
        }
        public IActionResult OnGet(string code = null, string email = null)
        {
            if (code == null || email == null)
            {
                _logger.LogWarning("Reset password code or email is null.");
                return BadRequest("Nu a fost gasit codul sau emailul pentru resetare.");
            }
            try
            {
                var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                Input = new InputModel
                {
                    Code = decodedCode,
                    Email = email
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la decodarea codului de resetare a parolei.");
                return BadRequest("Codul de resetare a parolei este invalid.");
            }
            _logger.LogInformation("Reset password page accessed for email: {Email}", email);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for reset password.");
                return Page();
            }
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found for email: {Email}", Input.Email);
                return RedirectToPage("./Confirmation", new { type = "passwordreset" });
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Password reset successful for user: {Email}", Input.Email);
                return RedirectToPage("./Confirmation", new { type = "passwordreset" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                _logger.LogError("Error resetting password: {Error}", error.Description);
            }

            return Page();
        }
    }
}

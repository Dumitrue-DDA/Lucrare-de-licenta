using Lucrare_de_licenta.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace Lucrare_de_licenta.Pages.Identity.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly UserManager<Utilizator> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(
            UserManager<Utilizator> userManager, IEmailSender emailSender, ILogger<ForgotPasswordModel> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                // !PENTRU PRODUCTIE!
                if (user == null) // || !(await _userManager.IsEmailConfirmedAsync(user)) - cu confirmarea mailului activata
                {
                    _logger.LogInformation("Utilizatorul este Null");
                    return RedirectToPage("./Conrirmation", new { type = "error" });

                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Identity/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code = code, email = Input.Email },
                    protocol: Request.Scheme);

                _logger.LogInformation("S-a generat link-ul de callback: {CallbackUrl}", callbackUrl);

                try
                {
                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Reset Password",
                        $"Va rugam resetati parola prinm <a href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>acest link</a>.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Eroare la trimiterea emailul de resetare a parolei");
                    ModelState.AddModelError(string.Empty, "A aparut o eroare in transmiterea emailului.");
                    return Page();
                }
                return RedirectToPage("./Confirmation", new { type = "emailsent" });
            }
            return Page();
        }
        public void OnGet()
        {
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lucrare_de_licenta.Pages.Identity.Account
{
    [AllowAnonymous]
    public class ConfirmationModel : PageModel
    {
        public string Title { get; set; } = "Confirmation";
        public string? Message { get; set; }
        public string? AdditionalInfo { get; set; }
        public string CallBackUrl { get; set; } = "/Index";
        public string AlertType { get; set; } = "success";
        public string ButtonText { get; set; } = "Continue";

        public IActionResult OnGet(
            string? title = null,
            string? message = null,
            string? type = null,
            string? additionalInfo = null,
            string? callBackUrl = null,
            string? buttonText = null,
            string alertType = "success"
            )
        {
            switch (type?.ToLower())
            {
                case "passwordreset":
                    Title = "Parola a fost resetata";
                    Message = "Parola a fost resetata cu succes.";
                    CallBackUrl = "/Identity/Account/Login";
                    ButtonText = "Spre pagina de conectare";
                    break;

                case "emailsent":
                    Title = "Email trimis";
                    Message = "Emailul a fost trimis cu succes. Verifica-ti inboxul.";
                    CallBackUrl = "/Index";
                    ButtonText = "Catre pagina principală";
                    break;

                case "emailconfirmation":
                    Title = "Email confirmat";
                    Message = "Emailul a fost confirmat cu succes.";
                    CallBackUrl = "/Identity/Account/Login";
                    ButtonText = "Spre pagina de conectare";
                    break;

                case "logout":
                    Title = "Deconectat";
                    Message = "V-ați deconectat cu succes.";
                    CallBackUrl = "/Index";
                    ButtonText = "Catre pagina principală";
                    break;

                case "registration":
                    Title = "Înregistrare reușită";
                    Message = "Contul a fost creat cu succes. Vă rugăm să vă autentificați.";
                    CallBackUrl = "/Identity/Account/Login";
                    ButtonText = "Spre pagina de conectare";
                    break;

                case "error":
                    Title = "Eroare";
                    Message = "A apărut o eroare. Încercați din nou sau contactați administratorul.";
                    CallBackUrl = "/Index";
                    ButtonText = "Catre pagina principală";
                    break;

                default:
                    Title = "Confirmare";
                    Message = "Operatiunea a fost finalizata cu succes.";
                    CallBackUrl = "/Index";
                    ButtonText = "Catre pagina principală";
                    break;
            }

            // Override with custom parameters if provided
            if (!string.IsNullOrEmpty(title)) Title = title;
            if (!string.IsNullOrEmpty(message)) Message = message;
            if (!string.IsNullOrEmpty(callBackUrl)) CallBackUrl = callBackUrl;
            if (!string.IsNullOrEmpty(additionalInfo)) AdditionalInfo = additionalInfo;

            return Page();
        }
    }
}

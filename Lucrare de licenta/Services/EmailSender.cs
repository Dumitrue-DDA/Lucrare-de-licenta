using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Lucrare_de_licenta.Services
{
    /// <summary>
    /// Serviciul ce se ocupa de trimis emailuri folosind SendGrid
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly SendGridOptions _sendGridOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor
        public EmailSender(IOptions<SendGridOptions> sendGridOptionsAccessor, ILogger<EmailSender> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _sendGridOptions = sendGridOptionsAccessor.Value;
            _httpClientFactory = httpClientFactory;

            if (string.IsNullOrEmpty(_sendGridOptions.ApiKey))
            {
                _logger.LogWarning("Cheia API pentru SendGrid nu este configurata. Emailul nu a fost trimis.");
            }
            if (string.IsNullOrEmpty(_sendGridOptions.FromEmail))
            {
                _logger.LogWarning("Emailul de transmitere pentru SendGrid nu a fost configurat. A fost folosita valoarea implicita.");
            }

            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// Trimite un email folosind setarile implicite.
        /// </summary>
        /// <param name="email">Emailul destinatar</param>
        /// <param name="subject">Subiectul/Titlul emailului</param>
        /// <param name="htmlMessage">Textul din email cu posibilitatea de a folosi html pentru stilizare</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("Constructor EmailSender: ApiKey = '{ApiKey}', FromEmail = '{FromEmail}'",
                _sendGridOptions.ApiKey ?? "NULL_OR_EMPTY",
                _sendGridOptions.FromEmail ?? "NULL_OR_EMPTY");

            if (string.IsNullOrEmpty(_sendGridOptions.ApiKey))
            {
                _logger.LogWarning("Cheia API pentru SendGrid lipseste. Emailul nu a fost trimis catre {Email}.", email);
                return;
            }
            if (string.IsNullOrEmpty(_sendGridOptions.FromEmail))
            {
                _logger.LogWarning("Emailul de transmitere pentru SendGrid lipseste. Emailul nu a fost trimis catre {Email}.", email);
                return;
            }

            _logger.LogInformation($"Se trimite mailu catre {email} cu subiectul '{subject}' de la adresa: {_sendGridOptions.FromEmail}");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _sendGridOptions.ApiKey);

            var emailData = new
            {
                personalizations = new[]
                {
                    new
                    {
                        to = new[] { new { email } },
                        subject
                    }
                },
                from = new { email = _sendGridOptions.FromEmail, name = _sendGridOptions.FromName ?? "Default Sender" },
                content = new[]
                {
                    new { type = "text/html", value = htmlMessage }
                }
            };

            var json = JsonSerializer.Serialize(emailData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                // Trimitem post request la api-ul sendgrid
                var response = await client.PostAsync(
                    "https://api.sendgrid.com/v3/mail/send",
                    content
                );
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Emailul a fost trimis cu succes catre {Email}", email);
                }
                else
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError(
                        "Nu a putut fi trimis mailul catre {Email}. Status Code: {StatusCode}, Body: {Body}",
                        email, response.StatusCode, errorBody
                        );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A aparut o eroare in incercarea de a trimite un mail catre {Email}", email);
                throw;
            }
        }
    }
}

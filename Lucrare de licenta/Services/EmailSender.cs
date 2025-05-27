using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Lucrare_de_licenta.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly SendGridOptions _sendGridOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        public EmailSender(IOptions<SendGridOptions> sendGridOptionsAccessor, ILogger<EmailSender> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _sendGridOptions = sendGridOptionsAccessor.Value;
            _httpClientFactory = httpClientFactory;

            if (string.IsNullOrEmpty(_sendGridOptions.ApiKey))
            {
                _logger.LogWarning("SendGrid API Key is not configured. Emails will not be sent.");
            }
            if (string.IsNullOrEmpty(_sendGridOptions.FromEmail))
            {
                _logger.LogWarning("SendGrid From Email is not configured. Default values will be used.");
            }

            _httpClientFactory = httpClientFactory;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("EmailSender constructor: ApiKey is '{ApiKey}', FromEmail is '{FromEmail}'",
                _sendGridOptions.ApiKey ?? "NULL_OR_EMPTY",
                _sendGridOptions.FromEmail ?? "NULL_OR_EMPTY");

            if (string.IsNullOrEmpty(_sendGridOptions.ApiKey))
            {
                _logger.LogWarning("SendGrid API Key is missing from options. Email not sent to {Email}.", email);
                return;
            }
            if (string.IsNullOrEmpty(_sendGridOptions.FromEmail))
            {
                _logger.LogWarning("SendGrid FromEmail is missing from options. Email not sent to {Email}.", email);
                return;
            }

            _logger.LogInformation($"Sending email to {email} with subject '{subject}' using From: {_sendGridOptions.FromEmail}");

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
                var response = await client.PostAsync(
                    "https://api.sendgrid.com/v3/mail/send",
                    content
                );
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Email sent successfully to {Email}", email);
                }
                else
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError(
                        "Failed to send email to {Email}. Status Code: {StatusCode}, Body: {Body}",
                        email, response.StatusCode, errorBody
                        );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending email to {Email}", email);
                throw;
            }
        }
    }
}

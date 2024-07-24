using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Sub.Models.Entities;

namespace Sub.Repository.EmaiRepository
{
    public class EmailService : IEmailService
    {
        private readonly SendGridSettings _sendGridSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SendGridSettings> sendGridSettings, ILogger<EmailService> logger)
        {
            _sendGridSettings = sendGridSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            var from = new EmailAddress("obergannikita@gmail.com", "Nikita");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            try
            {
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    var responseBody = await response.Body.ReadAsStringAsync();
                    _logger.LogError($"Failed to send email. StatusCode: {response.StatusCode}, ResponseBody: {responseBody}");
                    throw new Exception($"Failed to send email. StatusCode: {response.StatusCode}, ResponseBody: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
                throw new Exception("Error sending email", ex);
            }
        }
    }
}

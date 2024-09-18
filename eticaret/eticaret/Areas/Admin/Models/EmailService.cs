using System.Net.Mail;
using System.Net;

namespace eticaret.Areas.Admin.Models
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string recipientEmail, string subject, string body)
        {
            var smtpServer = "smtp.office365.com"; // Outlook için SMTP sunucusu
            var port = 587; // Outlook için genellikle kullanılan port
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];

            var client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(recipientEmail);

            try
            {
                client.Send(mailMessage);
            }
            catch (SmtpException ex)
            {
                // Hata mesajını loglayın veya yönetin
                throw new InvalidOperationException("E-posta gönderilirken bir hata oluştu.", ex);
            }
        }
    }
}

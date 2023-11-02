using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TravioHotel.Services
{
    public class MailServer
    {
        private string smtpServer = "smtp.gmail.com";
        private int smtpPort = 587;
        private string smtpUser = "Hassan2109f@aptechgdn.net";
        private string smtpPassword = "Bewafa124";

        public async Task Mail(string? toEmail, string? Subject, string? htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TravioTravel", smtpUser));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = Subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUser, smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }

}

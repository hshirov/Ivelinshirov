using Data.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Services.External
{
    public class MessageService : IMessageService
    {
        private readonly MailSettings _mailSettings;
        public MessageService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string fromDisplayName, string fromEmailAddress, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress(_mailSettings.ResiverName, _mailSettings.ResiverEmail));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = message
            };

            email.Body = body.ToMessageBody();

            using(var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback =
                    (sender, certificate, certChainType, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, false).ConfigureAwait(false);
                await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password).ConfigureAwait(false);

                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}

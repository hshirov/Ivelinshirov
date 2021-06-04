using Data.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Data;
using System.Threading.Tasks;

namespace Services.External
{
    public class MessageService : IMessageService
    {
        private readonly MailSettings _mailSettings;
        private readonly IContactInfoService _contactInfoService;
        private readonly string adminEmail;
        public MessageService(IOptions<MailSettings> mailSettings, IContactInfoService contactInfoService)
        {
            _mailSettings = mailSettings.Value;
            _contactInfoService = contactInfoService;

            adminEmail = _contactInfoService.Get().Result.ReceiverEmail;
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Ivelinshirov Admin", adminEmail));
            email.To.Add(new MailboxAddress(emailAddress, emailAddress));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };

            email.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
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

        public async Task SendEmailToAdminAsync(string fromDisplayName, string fromEmailAddress, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress("Ivelin Shirov", adminEmail));
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

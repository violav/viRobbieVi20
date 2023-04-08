using Core.Contracts;
using Core.Models;
using Microsoft.Extensions.Options;
using Services.Models.Contracts;
using Services.Providers;

namespace Services
{
    public class EmailService : IEmailClient
    {
        public EmailProvider _emailClient;

        public EmailService(IOptions<EmailLibrarySettings> emailLibrarySettings)
        {
            Smtp smtp = new(
                emailLibrarySettings.Value.SmtpClientHost,
                emailLibrarySettings.Value.SmtpClientPort,
                emailLibrarySettings.Value.SmtpClientUseSsl
            );
            EmailAuthentication authentication = new(
                emailLibrarySettings.Value.AuthenticationAddress,
                emailLibrarySettings.Value.AuthenticationPassword
            );
            Sender sender = new(
                emailLibrarySettings.Value.SenderAddress,
                emailLibrarySettings.Value.SenderName
            );
            _emailClient = new EmailProvider(smtp, authentication, sender);

        }

        public void SendEmail(Recipient recipient, Message msg)
        {
            _emailClient.SendEmail(recipient, msg);
        }
    }

}
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;
using Notifications.Application.Models.Email.Settings;
using System.Net;

namespace Notifications.Infraestruture.Email.Services
{
    public class NetMailService : IEmailService
    {
        private readonly EmailSpeceificSettings _credentialsKeySettings;

        public NetMailService(EmailSpeceificSettings credentialsKeySettings)
        {
            _credentialsKeySettings = credentialsKeySettings;
        }

        public async Task<bool> SendEmail(EmailToSendDto email,CancellationToken cancellationToken = default)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(
                _credentialsKeySettings.DisplayName,
                _credentialsKeySettings.Credentials.Email));

            mail.Sender = new MailboxAddress(
                _credentialsKeySettings.DisplayName,
                _credentialsKeySettings.Credentials.Email);

            foreach (var mailAddress in email.EmailsTo)
            {
                if (string.IsNullOrEmpty(mailAddress.DisplayName))
                    mail.To.Add(MailboxAddress.Parse(mailAddress.DisplayName));
                else
                    mail.To.Add(new MailboxAddress(mailAddress.DisplayName, (mailAddress.Email) ));
            }
           

            var body = new BodyBuilder();

            mail.Subject = email.Subject;
            if (email.Html)
            {
                body.HtmlBody = email.HtmlBody;
                mail.Body = body.ToMessageBody();
            }
            else
            {
                mail.Body = new TextPart("plain")
                {
                    Text = email.Message
                };
            }           

            try
            {
                using var smtp = new SmtpClient();

                smtp.CheckCertificateRevocation = false;
                if (_credentialsKeySettings.UseSSL)
                {
                    await smtp.ConnectAsync(_credentialsKeySettings.Host, _credentialsKeySettings.Port, SecureSocketOptions.SslOnConnect, cancellationToken);
                }
                else if (_credentialsKeySettings.UseStartTls)
                {
                    await smtp.ConnectAsync(_credentialsKeySettings.Host, _credentialsKeySettings.Port, SecureSocketOptions.StartTls, cancellationToken);
                }
              
                await smtp.AuthenticateAsync(new NetworkCredential(_credentialsKeySettings.Credentials.Email, _credentialsKeySettings.Credentials.GetPassword()), cancellationToken);

                await smtp.SendAsync(mail, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

   
}

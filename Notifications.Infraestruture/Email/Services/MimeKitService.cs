using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;
using System.Net;
using System.Runtime;

namespace Notifications.Infraestruture.Email.Services
{
    public class MimeKitService : IEmailService
    {
        public async Task<bool> SendEmail(EmailToSendDto email, ServerCredentialsConfiguration credentialsConfiguration, CancellationToken cancellationToken = default)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(
                credentialsConfiguration.config.DisplayName,
                credentialsConfiguration.credentials.UserName));

            mail.Sender = new MailboxAddress(
                email.DisplayName,
                email.EmailFrom);

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
                if (credentialsConfiguration.config.UseSSL)
                {
                    await smtp.ConnectAsync(credentialsConfiguration.config.Host, credentialsConfiguration.config.Port, SecureSocketOptions.SslOnConnect, cancellationToken);
                }
                else if (credentialsConfiguration.config.UseStartTls)
                {
                    await smtp.ConnectAsync(credentialsConfiguration.config.Host, credentialsConfiguration.config.Port, SecureSocketOptions.StartTls, cancellationToken);
                }
              
                await smtp.AuthenticateAsync(new NetworkCredential(credentialsConfiguration.credentials.UserName, credentialsConfiguration.credentials.GetPassword()), cancellationToken);

                await smtp.SendAsync(mail, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          

      

        }
    }

   
}

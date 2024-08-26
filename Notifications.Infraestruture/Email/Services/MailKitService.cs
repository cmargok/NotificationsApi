using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;
using Notifications.Application.Models.Email.Settings;
using System.Net;
using System.Net.Mail;

namespace Notifications.Infraestruture.Email.Services
{

    public class MailKitService : IEmailService
    {
        private readonly EmailSpeceificSettings _credentialsKeySettings;

        public MailKitService(EmailSpeceificSettings credentialsKeySettings)
        {
            _credentialsKeySettings = credentialsKeySettings;
        }
        public Task<bool> SendEmail(EmailToSendDto email, CancellationToken cancellationToken = default)
        {
            MailMessage mail;
            var success = false;

            if (email.Html)
            {
                mail = new MailMessage
                {
                    From = new MailAddress(_credentialsKeySettings.Credentials.Email, _credentialsKeySettings.DisplayName)
                };

                foreach (var emailTo in email.EmailsTo)
                {
                    mail.To.Add(emailTo.Email);
                }               
                mail.IsBodyHtml = true;
                mail.Body = email.HtmlBody;
                mail.Subject = email.Subject;
            }
            else
            {
                mail = new MailMessage(_credentialsKeySettings.Credentials.Email, email.EmailsTo[0].Email, email.Subject, email.Message);

            }


            // Enviar el correo electrónico
            try
            {
                using SmtpClient SmtpClient = new (
                    _credentialsKeySettings.Host,
                    _credentialsKeySettings.Port
                    );

                SmtpClient.Credentials = new NetworkCredential(
                   _credentialsKeySettings.Credentials.Email,
                   _credentialsKeySettings.Credentials.GetPassword()
                   );

                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.EnableSsl = _credentialsKeySettings.UseSSL;
                SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;               

                SmtpClient.Send(mail);

                success = true;
            }

            catch (Exception e)
            {
                //aqui iria logging
                Console.WriteLine(e.Message);
                success = false;
            }
            finally
            {
                mail.Dispose();

            }

            return Task.FromResult(success);
        }


    }
}

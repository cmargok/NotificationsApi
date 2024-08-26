using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;
using System.Net;
using System.Net.Mail;

namespace Notifications.Infraestruture.Email.Services
{

    public class MailKitService : IEmailService
    {

        public Task<bool> SendEmail(EmailToSendDto email, ServerCredentialsConfiguration credentialsConfiguration, CancellationToken cancellationToken = default)
        {
            MailMessage mail;
            var success = false;

            if (email.Html)
            {
                mail = new MailMessage
                {
                    From = new MailAddress(credentialsConfiguration.credentials.UserName, credentialsConfiguration.config.DisplayName)
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
                mail = new MailMessage(credentialsConfiguration.credentials.UserName, email.EmailsTo[0].Email, email.Subject, email.Message);

            }


            // Enviar el correo electrónico
            try
            {
                using SmtpClient SmtpClient = new (
                    credentialsConfiguration.config.Host, 
                    credentialsConfiguration.config.Port
                    );

                SmtpClient.Credentials = new NetworkCredential(
                   credentialsConfiguration.credentials.UserName,
                   credentialsConfiguration.credentials.GetPassword()
                   );

                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.EnableSsl = credentialsConfiguration.config.UseSSL;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;
using Notifications.Infraestruture.Externals.Azure;

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
                    From = new MailAddress(email.EmailFrom, credentialsConfiguration.config.DisplayName)
                };
                mail.To.Add(email.EmailTo);
                mail.IsBodyHtml = true;
                mail.Body = email.HtmlBody;
                mail.Subject = email.Subject;
            }
            else
            {
                mail = new MailMessage(email.EmailFrom, email.EmailTo, email.Subject, email.Message);

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

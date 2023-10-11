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

    public class NetMailService : IEmailService
    {

        public async Task<bool> SendEmail(EmailToSendDto email, OutlookCredentials credentials)
        {
            MailMessage mail;
            var success = false;
            if (email.Html)
            {
                mail = new MailMessage
                {
                    From = new MailAddress(email.EmailFrom, "kevin")
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

                using SmtpClient cliente = new("smtp.office365.com", 587);


                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new NetworkCredential(credentials.Email, credentials.GetPassword());
                cliente.EnableSsl = true;
                cliente.DeliveryMethod = SmtpDeliveryMethod.Network;

                cliente.Send(mail);
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

            return success;
        }


    }
}

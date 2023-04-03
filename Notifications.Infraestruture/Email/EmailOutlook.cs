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

namespace Notifications.Infraestruture.Email
{
    public class EmailOutlook: IEmailOutlook
    {
        public async Task<RemitenteData> SetRemitenteData(bool IsDevelopment, string KeyVaultUrlEnviroment, string FileName,params string[] secrets)
        {

            var data = await SecretsManager.GetRemitenteDataAsync(IsDevelopment, KeyVaultUrlEnviroment, FileName,secrets);

            var tempData = data[0].Split('=');

            var remitente = new RemitenteData { Email = tempData[1] };

            tempData = data[1].Split('=');

            remitente.SetPassword(tempData[1]);

            return remitente;
        }

        public Task<bool> SendEmail(EmailToSendDto email, RemitenteData remitente)
        {
            MailMessage mail;

            if (email.Html)
            {
                mail = new MailMessage();
                mail.From = new MailAddress(remitente.Email);
                mail.To.Add(email.EmailDestinatario);
                mail.IsBodyHtml = true;
                mail.Body = email.HtmlBody;
                mail.Subject = email.Asunto;
            }
            else
            { 
                mail = new MailMessage(remitente.Email, email.EmailDestinatario, email.Asunto, email.Mensaje);
            }


            // Enviar el correo electrónico
            try
            {
                SmtpClient cliente = new("smtp.office365.com", 587);

                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new NetworkCredential(remitente.Email, remitente.GetPassword());
                cliente.EnableSsl = true;

                cliente.Send(mail);
                return Task.FromResult(true);
            }

            catch (Exception e)
            {
                //aqui iria logging

                return Task.FromResult(false);
            }

        }

        
    }
}

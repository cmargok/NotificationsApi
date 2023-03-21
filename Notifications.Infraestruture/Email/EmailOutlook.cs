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

            var remitente = new RemitenteData { Email = tempData[0] };

            tempData = data[1].Split('=');

            remitente.SetPassword(tempData[0]);

            return remitente;
        }

        public async Task<bool> SendEmail(EmailToSendDto email, RemitenteData remitente)
        {

                        // Configurar el cliente de correo electrónico
            SmtpClient cliente = new("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(remitente.Email, remitente.GetPassword()),
                EnableSsl = true,

            };
            MailMessage mail;

            if (email.Html)
            {
                mail = new MailMessage();
                mail.From = new MailAddress(remitente.Email);
                mail.To.Add(email.EmailDestinatario);
                mail.IsBodyHtml = true;
                mail.Body = "<h1>Este es un ejemplo de correo electrónico en formato HTML</h1>" +
                   "<p>Este es un párrafo de texto.</p>" +
                   "<ul>" +
                   "<li>Item 1</li>" +
                   "<li>Item 2</li>" +
                   "<li>Item 3</li>" +
                   "</ul>";
            }
            else
            {
                // Crear el mensaje de correo electrónico
                mail = new MailMessage(remitente.Email, email.EmailDestinatario, email.Asunto, email.Mensaje);
            }


            // Enviar el correo electrónico
            try
            {
                await cliente.SendMailAsync(mail);
                return true;
            }
            catch (Exception e)
            {
                //aqui iria logging

                return false;
            }

        }

        
    }
}

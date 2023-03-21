using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;

namespace Notifications.Infraestruture.Email
{
    public class EmailOutlook: IEmailOutlook
    {
        public async Task<bool> SendEmail(EmailToSendDto email, RemitenteData remitente)
        {

            // string destinatario = "cmargokk@gmail.com";
            //  string asunto = "Prueba de correo electrónico desde .NET 6";
            //  string cuerpo = "Este es un correo electrónico de prueba enviado desde una aplicación .NET 6 utilizando Outlook.";


            // Configurar el cliente de correo electrónico
            SmtpClient cliente = new("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(remitente.Email, remitente.GetPassword()),
                EnableSsl = true,

            };
            MailMessage mensaje;

            if (email.Html)
            {
                mensaje = new MailMessage(remitente.Email, email.EmailDestinatario, email.Asunto, email.HtmlBody);
            }
            else
            {
                // Crear el mensaje de correo electrónico
                mensaje = new MailMessage(remitente.Email, email.EmailDestinatario, email.Asunto, email.Mensaje);
            }


            // Enviar el correo electrónico
            try
            {
                await cliente.SendMailAsync(mensaje);
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

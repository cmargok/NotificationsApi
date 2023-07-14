using Notifications.Application.Email;
using Notifications.Application.Models.Email;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Quarzo
{
    public interface Imio
    {
        public  Task<bool> ahuevo();

    }
    public  class CycleService : Imio
    {
        private readonly IEmailManager _emailManager;
        public CycleService(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        

   
        public async Task<bool> ahuevo()
        {
            string CodeAccess = "as6d5a132s1d98a41sd";
            string CodeIV = "asd9a8sd4a36516154@easdfsdf";
            string name = "Brayan Betancourth";

            string htmlBody = @"<!DOCTYPE html>
                                <html>

                                <head>
                                    <meta charset=""utf-8"">
                                    <title>Email Validation</title>
                                </head>

                                <body>
                                    <p>Hola {0}, ¿How is it going? </p>
                                    <p>Thnk you for starting the registration our platform
                                        <br> We are sending you the <stron>AccessCode</strong> and the Code IV to continue the proccess:
                                    </p>
                                    <ul>
                                        <li><strong>AccessCode:</strong> {1}</li>
                                        <li><strong>Code IV:</strong> {2}</li>
                                    </ul>
                                    <p>Thanks a lot for your time.</p>
                                    <p>Cmargok Security System.</p>
                                </body>

                                </html>";

            var htmlSBody = string.Format(htmlBody, name, CodeAccess, CodeIV);

            var emailToSend = new EmailToSendDto
            {
                Asunto = "prueb aquertizto",
                EmailDestinatario = "cmargokk@gmail.com",
                Html = true,
                HtmlBody = htmlSBody,
                Mensaje = "a"
            };

            emailToSend.HtmlBody = htmlSBody;
            var success = await _emailManager.SendEmail(emailToSend);

            return true;
        }
    }

    public class RecurrentJob : IJob
    {
        private readonly Imio _mio;
        public RecurrentJob(Imio mio)
        {
            _mio = mio;
        }
        public Task Execute(IJobExecutionContext context)
        {
            DateTimeOffset fechaActualUtc = DateTimeOffset.UtcNow;
   
           
                Console.WriteLine(" el email ha sido enviado a " +fechaActualUtc.DateTime);

                _mio.ahuevo();
          
            return Task.CompletedTask;
        }
    }
}

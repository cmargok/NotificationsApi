using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Email;
using Notifications.Application.Models.Email;

namespace Notifications.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmailController : ControllerBase
    {
        

        private readonly IEmailManager _emailManager;
        public EmailController( IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        [HttpPost("Outlook")]
        public async Task<IActionResult> SendEmailFromOutlook(EmailToSendDto emailToSend)
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

            emailToSend.HtmlBody = htmlSBody;
            var success = await _emailManager.SendEmail(emailToSend);

            if (success)
            {
                return Ok("sent");

            }

            return BadRequest();
        }
    }
}
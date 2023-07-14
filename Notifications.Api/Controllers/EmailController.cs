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

        

            var success = await _emailManager.SendEmail(emailToSend);

            if (success)
            {
                return Ok("sent");

            }

            return BadRequest();
        }

        [HttpGet("id")]
        public async Task<IActionResult> A(int id)
        {

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Asa(int id)
        {

            return Ok();
        }

    }
}
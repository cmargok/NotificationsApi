using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;
using Notifications.Application.Utils;

namespace Notifications.Api.Controllers
{
    [ApiController]
    [Route("api/v1/notification")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DeliverEmailController : ControllerBase
    {   
        private readonly IEmailManager _emailManager;

      
        public DeliverEmailController(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

  
       // [Authorize]
        [HttpPost("sendEmail")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendEmailAsync(EmailToSendDto emailToSend)
        {        

            var result = await _emailManager.SendEmailAsync(emailToSend);

            if (result.Error)
                return BadRequest(result);

            return Ok(result);
        }



     

    }

   
}
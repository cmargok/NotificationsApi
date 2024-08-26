using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Notifications.Api.Middleware;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Notifications.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmailController : ControllerBase
    {   
        private readonly IEmailManager _emailManager;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailManager"></param>
        public EmailController(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailToSend"></param>
        /// <returns></returns>
       // [Authorize]
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync(EmailToSendDto emailToSend)
        {        

            var success = await _emailManager.SendEmail(emailToSend);

            if (success)
            {
                return Ok("sent");

            }

            return BadRequest();
        }



     

    }

   
}
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
        private readonly IModerna mo;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailManager"></param>
        public EmailController(IEmailManager emailManager, IModerna mo)
        {
            _emailManager = emailManager;
            this.mo = mo;
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


        //aqui estamos probando creacion y ambientacion de JWT
        [Authorize]
        [HttpGet("Id")]
        public async Task<IActionResult> A(int id)
        {

            return Ok(id);
        }

        [HttpGet("MyToken")]
        public async Task<IActionResult> Asa(int id)
        {

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, "Kevinzin"),
                new Claim(ClaimTypes.Role, "SUper"),
                new Claim("UserType", "authorized")
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("G3VF4C6KFV43JH6GKCDFGJH45V36JHGV3H4C6F3GJC63HG45GH6V345GHHJ4623FJL3HCVMO1P23PZ07W8"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "telefonica.com.co",
                audience: "Public",
                expires: DateTime.UtcNow.AddMinutes(50),
                            
                claims: claims,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);      

            var tokenString2 = await mo.Caller();

            return Ok(tokenString);
        }

    }

    public interface IModerna 
    {
        Task<string> Caller();
    }

    public class Moderna : IModerna
    {
        private readonly HttpClient _httpClient;
       // private readonly ICorrelationIdSentinel _correlationIdSentinel;
        //private const string _correlationIdHeader = ;

        public Moderna(HttpClient httpClient/*, ICorrelationIdSentinel correlationIdSentinel*/)
        {
            _httpClient = httpClient;
            //_correlationIdSentinel = correlationIdSentinel;
        }

        public async Task<string> Caller()
        {
        
           // var response = await _httpClient.GetAsync("https://localhost:7220/api/limpio");

           // var content = await response.Content.ReadAsStringAsync();

            return "4";
        }
    }
}
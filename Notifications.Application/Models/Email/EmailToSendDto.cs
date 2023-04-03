using System.ComponentModel.DataAnnotations;

namespace Notifications.Application.Models.Email
{
    public class EmailToSendDto
    {
        [EmailAddress]
        public string EmailDestinatario { get; set; } = string.Empty;
        public string Asunto { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public bool Html { get; set; } = false;
        public string HtmlBody { get; set; } = string.Empty;

    }
}
using System.ComponentModel.DataAnnotations;

namespace Notifications.Application.Models.Email
{
    public class EmailToSendDto
    {
        [EmailAddress]
        public string EmailFrom { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailTo { get; set; } = string.Empty;
        public string Subject{ get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Html { get; set; } = false;
        public string HtmlBody { get; set; } = string.Empty;

    }
}
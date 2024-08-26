namespace Notifications.Application.Models.Email.Settings
{
    public class MailSettings
    {
        public required string ServiceName { get; set; }
        public required ServerConfig ServerConfiguration { get; set; }

    }
}

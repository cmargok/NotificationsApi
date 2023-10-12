namespace Notifications.Application.Models.Email.Settings
{
    public class MailSettings
    {
        public string? ServiceName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public ServerConfig ServerConfiguration { get; set; }

        public string[] GetCredentials() => [UserName, Password];
    }
}

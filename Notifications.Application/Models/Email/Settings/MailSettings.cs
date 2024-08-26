using Notifications.Application.Utils.Enums;

namespace Notifications.Application.Models.Email.Settings
{
    public class MailSettings
    {
        public required EnumMailServices ServiceName { get; set; }
        public required SharedConfig SharedConfig { get; set; }
        public required List<EmailService> Services { get; set; }        
        public required AuthEmailSettings AuthEmailSettings { get; set; }


    }
    public class SharedConfig
    {
        public string? DisplayName { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; } = -1;
        public string? User { get; set; }

    }

    public class EmailService
    {
        public string ServiceName { get; set; } = string.Empty;
        public required ServerConfig ServerConfiguration { get; set; }
    }

    public class ServerConfig
    {
        public bool UseSSL { get; set; } = false;
        public bool UseStartTls { get; set; } = false;
    }

    public class AuthEmailSettings
    {
        public required string Pass { get; init; }
        public required string Email { get; init; }
    }
}

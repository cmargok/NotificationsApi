namespace Notifications.Application.Models.Email.Settings
{
    public class ServerConfig
    {
        public string? DisplayName { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; } = -1;
        public string? User { get; set; }
        public bool UseSSL { get; set; } = false;
        public bool UseStartTls { get; set; } = false;

    }
}

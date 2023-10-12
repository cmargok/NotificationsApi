using Notifications.Application.Models.Email.Settings;

namespace Notifications.Application.Models.Email
{
    public class ServerCredentialsConfiguration
    {
        public Credentials credentials { get; set; }
        public ServerConfig config { get; set; }
    }
}

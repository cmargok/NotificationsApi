using System.Security;
using System.ComponentModel.DataAnnotations;

namespace Notifications.Application.Models.Email
{
    public class RemitenteData
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        private SecureString Password;


        public SecureString GetPassword() { return Password; }

        public void SetPassword(string password)
        {

            Password = new SecureString();

            foreach (char c in password)
            {
                Password.AppendChar(c);
            }
        }
    }
}

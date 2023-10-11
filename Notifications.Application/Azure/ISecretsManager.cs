using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Azure
{
    public interface ISecretsManager
    {
        public Task<string> GetConnectionString(string connection, string KeyVaultUrlEnviroment = "");

        public Task<Dictionary<string, string>> GetSecretsAsync(string KeyVaultUrlEnviroment, params string[] secrets);

    }
}

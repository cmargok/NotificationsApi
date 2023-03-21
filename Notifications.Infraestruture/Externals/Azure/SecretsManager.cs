using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Notifications.Application.Models.Email;
using Notifications.Infrastructure.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infraestruture.Externals.Azure
{
    public class SecretsManager
    {
        // private static readonly string SecurityDB = "SecurityDbConnection";
        public static async Task<string> GetConnectionString(bool IsDevelopment, string connection, string KeyVaultUrlEnviroment = "")
        {
            ArgumentException.ThrowIfNullOrEmpty(connection);

            if (IsDevelopment)
            {
                string path = "C:\\Secrets";

                var connectionString = IOReader.GetOneLineFromFile(path, connection + ".txt");

                return connectionString;

            }

            return await GetValueFromSeret(connection, KeyVaultUrlEnviroment);

        }

        public static async Task<List<string>> GetRemitenteDataAsync(bool IsDevelopment, string KeyVaultUrlEnviroment, string FileName, params string[] secrets)
        {

            List<string> dataSecrets = new();
            if (IsDevelopment)
            {
                string path = "C:\\Secrets";
                
                dataSecrets = IOReader.GetLinesFromFile(path, FileName + ".txt");

                if (dataSecrets.Count != 0) throw new FileLoadException();
                
                return dataSecrets;
            }
           
            foreach (var secret in secrets)
            {
                dataSecrets.Add(await GetValueFromSeret(secret, KeyVaultUrlEnviroment));
            }
            if (dataSecrets.Count != 0) throw new Exception("No secrets loaded");        

            return dataSecrets;
        }

        private static async Task<string> GetValueFromSeret(string SecretName, string KeyVaultUrlEnviroment)
        {
            var keyVaultEndpoint = new Uri(KeyVaultUrlEnviroment!.ToString());
            //  var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("KeyVaultUrl")!.ToString());

            var _secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

            var secret = await _secretClient.GetSecretAsync(SecretName);

            return secret.Value.Value;
        }
    }

}

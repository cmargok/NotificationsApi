using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Notifications.Infraestruture.Externals.Azure
{

    public class SecretsManager 
    {
        public async Task<string> GetConnectionString(string connection, string KeyVaultUrlEnviroment = "")
        {
            var azureConnector = AzureConector.GetMyConnector();

            ArgumentException.ThrowIfNullOrEmpty(connection);

            return await azureConnector.GetOneValueFromSeret(connection, KeyVaultUrlEnviroment);

        }

        public async Task<Dictionary<string, string>> GetSecretsAsync(string KeyVaultUrlEnviroment, params string[] secrets)
        {
            ArgumentException.ThrowIfNullOrEmpty(KeyVaultUrlEnviroment);

            var azureConnector = AzureConector.GetMyConnector();

            var secretsDictionary = await azureConnector.GetListValueFromSeret(secrets.ToList(), KeyVaultUrlEnviroment);

            return secretsDictionary;
        }

        
    }

    public class AzureConector
    {
        static AzureConector() { }

        public static AzureConector GetMyConnector() 
        {
            return new AzureConector();        
        }

        public async Task<string> GetOneValueFromSeret(string SecretName, string KeyVaultUrlEnviroment)
        {
            var keyVaultEndpoint = new Uri(KeyVaultUrlEnviroment!.ToString());

            var _secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

            var secret = await _secretClient.GetSecretAsync(SecretName);

            return secret.Value.Value;
        }

        public async Task<Dictionary<string, string>> GetListValueFromSeret(List<string> SecretsName, string KeyVaultUrlEnviroment)
        {
            var keyVaultEndpoint = new Uri(KeyVaultUrlEnviroment!.ToString());

            var _secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

            var secretsDictionary = new Dictionary<string, string>();

            if(SecretsName is null) throw new ArgumentNullException(nameof(SecretsName));

            foreach (var secretName in SecretsName)
            {
                var AzureSecret = await _secretClient.GetSecretAsync(secretName);

                secretsDictionary.Add(secretName, AzureSecret.Value.Value);
            }          
            
            return secretsDictionary;
        }
    }

}

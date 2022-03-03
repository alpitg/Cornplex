
using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace Cornplex.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var setting = config.Build();

                    // NOTE: Use these packages for KeyVault
                    // 1. Azure.Identity
                    // 2. Azure.Security.KeyVault.Secrets

                    var clientId = setting["KeyVault:ClientId"];
                    var clientSecret = setting["KeyVault:ClientSecret"];
                    var keyVaultEndpoint = setting["KeyVault:Endpoint"];

                    // NOTE: Different ways to access KeyVault
                    // default is the best way so far
                    var way = "way2";

                    switch (way)
                    {
                        case "way1":
                            // NOTE: DEPRICATED - Microsoft.Extensions.Configuration.AzureKeyVault
                            // NOTE: Connect to Azure Key Vault using the Client Id and Client Secret (AAD) - Get them from Azure AD Application.
                            if (!string.IsNullOrEmpty(keyVaultEndpoint) && !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
                            {
                                config.AddAzureKeyVault(keyVaultEndpoint, clientId, clientSecret, new DefaultKeyVaultSecretManager());
                            }
                            break;

                        case "way2":
                            // NOTE: In case you want the read a secret value, use this.
                            var tenantId = setting["KeyVault:TenantId"];
                            const string secretName = "dev-connectionstring";
                            var clientCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                            var client = new SecretClient(new Uri(keyVaultEndpoint), clientCredential);
                            var value = client.GetSecret(secretName).Value.Value;
                            Console.Write(value);
                            break;

                        default:
                            // NOTE: Connect to Azure Key Vault using the Client Id and Client Secret (AAD) - Get them from Azure AD Application.
                            config.AddAzureKeyVault(keyVaultEndpoint, clientId, clientSecret);
                            break;
                    }



                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    //.UseUrls("http://*:5000;https://*:5001") // for production: use port 5001
                    .UseUrls("http://*:5000") // for development: use port 5000
                    .UseStartup<Startup>();
                });
    }
}

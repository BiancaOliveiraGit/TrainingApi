using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.DependencyInjection;

namespace TrainingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((ctx, builder) =>
               {
                   var keyVaultEndpoint = GetKeyVaultEndpoint();
                   if (!string.IsNullOrEmpty(keyVaultEndpoint))
                   {
                       var azureServiceTokenProvider = new AzureServiceTokenProvider();
                       var keyVaultClient = new KeyVaultClient(
                           new KeyVaultClient.AuthenticationCallback(
                               azureServiceTokenProvider.KeyVaultTokenCallback));
                       builder.AddAzureKeyVault(
                           keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                   }
               })
             .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
             .ConfigureServices(serviceCollection => serviceCollection
                .Configure<AzureFileLoggerOptions>(options =>
                {
                    options.FileName = "azure-diagnostics-";
                    options.FileSizeLimit = 50 * 1024;
                    options.RetainedFileCountLimit = 5;
                }).Configure<AzureBlobLoggerOptions>(options =>
                {
                    options.BlobName = "logApi.txt";
                }))
             .UseStartup<Startup>()
             .Build();
       
        private static string GetKeyVaultEndpoint() => "https://exerciselocalvault.vault.azure.net";

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            builder.Sources.Clear();
           // builder.AddJsonFile("config.json", false, true); Remove because using KeyVault Secrets
        }
    }
}



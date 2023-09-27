using Microsoft.Azure.Amqp;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReserveOrderFinal.Interface;
using ReserveOrderFinal.Models;
using ReserveOrderFinal.Models.ConfigModels;
using ReserveOrderFinal.Services;

public class program {
    public static async Task Main(string[] args)
    {
        var builder = Host
          .CreateDefaultBuilder(args)
          .ConfigureFunctionsWorkerDefaults()
          .ConfigureAppConfiguration((hostingContext, configBuilder) =>
          {
              var env = hostingContext.HostingEnvironment;
              
          })
          .ConfigureServices((appBuilder, services) =>
          {
            
             // var  = appBuilder.Configuration;
              var configuration = new ConfigurationBuilder()
                              .SetBasePath(Environment.CurrentDirectory)
                              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                              .AddEnvironmentVariables()
                              .Build();
              var Blobserviceconfig = .Get<BlobserviceConfig>();
              var emailserviceconfig = configuration.GetSection("appsetting:SendEmailService").Bind(opt);
              services.Configure<BlobserviceConfig>(opt => configuration.GetSection("ReserveOrderService").Bind(opt));
              services.Configure<EmailServiceConfig>(opt => configuration.GetSection("SendEmailService").Bind(opt));
              services.AddOptions();

              services.AddAzureClients(builder =>
              {
                  builder.AddBlobServiceClient(configuration.GetSection("BlobConnectionString"));
              });
              services.AddHttpClient("SendEmail", options =>
              {
                  options.BaseAddress = new System.Uri(configuration.GetSection("LogicAppURI").Value);
              });
              services.AddScoped<IBlobService, BlobService>();
              services.AddSingleton<IEmailService, EmailService>();
              services.AddTransient<EmailTemplate>();
          });

        await builder.Build().RunAsync();
    }
}


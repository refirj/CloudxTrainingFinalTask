using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class program
{
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
              services.AddOptions();

              services.AddSingleton<CosmosClient>(serviceProvider =>
              {

                  string key = configuration.GetSection("CosmosKey").Value;
                  return new CosmosClient(key);

              });
              
          });

        await builder.Build().RunAsync();
    }

}














///////////////////////////////////////////////////////////////////////////////////////////////////
///




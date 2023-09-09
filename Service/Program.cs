using System.Data;
using MySql.Data.MySqlClient;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Retry.Simple;
using Service.Handlers;

var hostBuilder = Host.CreateDefaultBuilder(args);

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

hostBuilder.ConfigureServices(services =>
{
  services.AddRebus(configure =>
    configure
      .Options(options =>
      {
        options.SetBusName("DMSF ServiceBus");
        options.SimpleRetryStrategy();
      })
      .Subscriptions(_ => _.StoreInMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSFBusSubscriptions"))
      .Transport(_ => _.UseMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSFBus", "DMSF_Service"))
      .Logging(_ => _.ColoredConsole()));
  services.AddRebusHandler<EMailSentEventHandler>();

  services.AddSingleton<IDbConnection>(_ => new MySqlConnection(configuration.GetSection("connectionStrings")["ServiceDatabase"]));
  services.AutoRegisterHandlersFromAssemblyOf<Program>();
});

var host = hostBuilder.Build();

host.Run();
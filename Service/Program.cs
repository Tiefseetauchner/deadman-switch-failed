using System.Data;
using DeadManSwitchFailed.Common.ServiceBus.Events;
using DeadManSwitchFailed.Service.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Retry.Simple;

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
        .Subscriptions(_ => _.StoreInMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSF_Service", true))
        .Transport(_ => _.UseMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSFBus", "DMSF_Service"))
        .Logging(_ => _.ColoredConsole()),
    onCreated: async bus => { await bus.Subscribe<SendEMailEvent>(); });

  services.AddSingleton<IDbConnection>(_ => new MySqlConnection(configuration.GetSection("connectionStrings")["ServiceDatabase"]));
  services.AutoRegisterHandlersFromAssemblyOf<Program>();
});

var host = hostBuilder.Build();

var xyz = new EMailSentEventHandler(host.Services.GetService<IBus>(), host.Services.GetService<IDbConnection>());

await xyz.Handle(null);

host.Run();
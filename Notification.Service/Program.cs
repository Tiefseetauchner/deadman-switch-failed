using System.Data;
using DeadManSwitchFailed.Common.Email;
using DeadManSwitchFailed.Common.ServiceBus.Events;
using DeadManSwitchFailed.Service.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Rebus.Config;
using Rebus.Retry.Simple;

var hostBuilder = Host.CreateDefaultBuilder(args);

var connectionStringConfiguration = new ConnectionStringConfiguration();
var mailServerConfiguration = new MailServerConfiguration();

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

configuration.Bind("connectionStrings", connectionStringConfiguration);
configuration.Bind("mailServer", mailServerConfiguration);

hostBuilder.ConfigureServices(services =>
{
  services.AddRebus(configure =>
      configure
        .Options(options =>
        {
          options.SetBusName("DMSF ServiceBus");
          options.SimpleRetryStrategy();
        })
        .Subscriptions(_ => _.StoreInMySql(connectionStringConfiguration.BusDatabase, "DMSF_Service", true))
        .Transport(_ => _.UseMySql(connectionStringConfiguration.BusDatabase, "DMSFBus", "DMSF_Service"))
        .Logging(_ => _.ColoredConsole()),
    onCreated: async bus => { await bus.Subscribe<SendEMailEvent>(); });

  services.AddSingleton<IDbConnection>(_ => new MySqlConnection(connectionStringConfiguration.ServiceDatabase));

  services.AddTransient<ISmtpClientFactory>(_ =>
    new SmtpClientFactory(
      mailServerConfiguration.Host,
      mailServerConfiguration.Port,
      mailServerConfiguration.Timeout,
      mailServerConfiguration.Username,
      mailServerConfiguration.Password));

  services.AutoRegisterHandlersFromAssemblyOf<Program>();
});

var host = hostBuilder.Build();

host.Run();
using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddRebus(configure =>
  configure
    .Options(options =>
    {
      options.SetBusName("DMSF ServiceBus");
      options.SimpleRetryStrategy();
    })
    .Subscriptions(_ => _.StoreInMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSFBusSubscriptions"))
    .Transport(_ => _.UseMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSFBus", "DMSF_Service"))
    .Logging(_ => _.Console()));

services.AddSingleton<IDbConnection>(_ => new MySqlConnection(configuration.GetSection("connectionStrings")["WebDatabase"]));
services.AutoRegisterHandlersFromAssemblyOf<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
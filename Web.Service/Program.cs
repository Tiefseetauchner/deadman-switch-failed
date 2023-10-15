using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Rebus.Config;
using Rebus.Retry.Simple;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

var services = builder.Services;
services.AddControllersWithViews();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddRebus(configure =>
  configure
    .Options(options =>
    {
      options.SetBusName("DMSF ServiceBus");
      options.SimpleRetryStrategy();
    })
    .Subscriptions(_ => _.StoreInMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSF_Service", true))
    .Transport(_ => _.UseMySql(configuration.GetSection("connectionStrings")["BusDatabase"], "DMSFBus", "DMSF_Service"))
    .Logging(_ => _.ColoredConsole()));

services.AddSingleton<IDbConnection>(_ => new MySqlConnection(configuration.GetSection("connectionStrings")["WebDatabase"]));
services.AutoRegisterHandlersFromAssemblyOf<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
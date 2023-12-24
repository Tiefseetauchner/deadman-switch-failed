using System.Text.Json.Serialization;
using DeadmanSwitchFailed.Common.Email;
using DeadmanSwitchFailed.Services.Vault.Service.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeadmanSwitchFailed.Services.Notification.Service
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var mailServerConfiguration = builder.Configuration.GetSection("mailServer");
      var connectionStrings = builder.Configuration.GetSection("connectionStrings");

      builder.Services.AddSingleton<ISmtpClientFactory>(_ => new SmtpClientFactory(
        mailServerConfiguration["host"],
        mailServerConfiguration.GetValue<int>("port"),
        mailServerConfiguration.GetValue<int>("timeout"),
        mailServerConfiguration["username"],
        mailServerConfiguration["password"]));

      builder.Services.AddDbContextPool<VaultContext>(_ =>
        _.UseMySql(connectionStrings["ServiceDatabase"], MariaDbServerVersion.LatestSupportedServerVersion));

      builder.Services.AddScoped(_ =>
        new UnitOfWork(_.GetService<VaultContext>()));

      builder.Services.ConfigureHttpJsonOptions(_ =>
      {
        _.SerializerOptions.IncludeFields = true;
        _.SerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
      });

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
    }
  }
}
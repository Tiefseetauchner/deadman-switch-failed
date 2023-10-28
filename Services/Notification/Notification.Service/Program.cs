using System.Data;
using Dapper;
using DeadmanSwitchFailed.Common.Domain;
using DeadmanSwitchFailed.Common.Email;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;

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

      builder.Services.AddScoped<IDbConnection, MySqlConnection>(_ => new MySqlConnection(connectionStrings["ServiceDatabase"]));

      builder.Services.AddSingleton<ISmtpClientFactory>(_ => new SmtpClientFactory(
        mailServerConfiguration["host"],
        mailServerConfiguration.GetValue<int>("port"),
        mailServerConfiguration.GetValue<int>("timeout"),
        mailServerConfiguration["username"],
        mailServerConfiguration["password"]));

      builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

      SetupDapper();

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

    private static void SetupDapper()
    {
      SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }
  }
}
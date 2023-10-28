using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DeadmanSwitchFailed.Common.ArgumentChecks;
using DeadmanSwitchFailed.Common.Email;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;

public class NotificationRepository : INotificationRepository
{
  private readonly IDbConnection _connection;
  private readonly ISmtpClientFactory _smtpClientFactory;

  public NotificationRepository(IDbConnection connection, ISmtpClientFactory smtpClientFactory)
  {
    _connection = connection;
    _connection.Open();
    _smtpClientFactory = smtpClientFactory;
  }

  public async Task<IEnumerable<Models.Notification>> GetNotificationsByVaultIdAsync(Guid id)
  {
    var notifications = await _connection.GetAllAsync<PersistentNotification>();

    return GetNotifications(notifications);
  }

  private IEnumerable<Models.Notification> GetNotifications(IEnumerable<PersistentNotification> persistentNotifications) =>
    persistentNotifications.Select(GetCastNotification);

  private Models.Notification GetCastNotification(PersistentNotification persistentNotification) =>
    persistentNotification.Type switch
    {
      NotificationType.Email => JsonSerializer.Deserialize<EmailNotification>(persistentNotification.ContainedData),
      _ => throw new ArgumentOutOfRangeException()
    };

  public async Task<IEnumerable<Models.Notification>> CreateNotification(Models.Notification notification)
  {
    return await _connection.GetAllAsync<Models.Notification>();
  }

  public Task MarkNotificationAsSent(Guid id) =>
    throw new NotImplementedException();

  public Task<Models.Notification> GetById(Guid id) =>
    throw new NotImplementedException();

  public async Task<Guid> CreateEmail(EmailNotification notification)
  {
    // var jsonOptions = new JsonSerializerOptions()
    // {
    //   IncludeFields = true
    // };

    await _connection.InsertAsync(new PersistentNotification
    {
      Type = notification.CheckNotNull().NotificationType,
      ContainedData = JsonSerializer.SerializeToUtf8Bytes(notification),
      VaultId = Guid.NewGuid(),
    });

    return Guid.NewGuid();
  }
}
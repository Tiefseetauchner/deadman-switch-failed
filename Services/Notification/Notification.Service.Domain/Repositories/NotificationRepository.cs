using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.ArgumentChecks;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;

public class NotificationRepository : INotificationRepository
{
  private readonly NotificationContext _context;

  public NotificationRepository(NotificationContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Models.Notification>> GetNotificationsByVaultIdAsync(Guid id) =>
    (await _context.Notifications.AsQueryable()
      .Where(_ => _.VaultId == id)
      .ToListAsync())
    .Select(GetAggregate);

  public async Task<Guid> CreateEmailNotification(Models.EmailNotification notification) =>
    (await _context.AddAsync(GetPersistent(notification.CheckNotNull()))).Entity.Id;

  public Task MarkNotificationAsSent(Guid id) =>
    throw new NotImplementedException();

  public Task<Models.Notification> GetById(Guid id) =>
    throw new NotImplementedException();


  private Models.Notification GetAggregate(PersistentNotification persistentNotification)
  {
    var notification = persistentNotification.Type switch
    {
      NotificationType.Email => JsonSerializer.Deserialize<EmailNotification>(persistentNotification.ContainedData),
      _ => throw new ArgumentOutOfRangeException()
    };

    notification.Id = persistentNotification.Id;
    notification.VaultId = persistentNotification.VaultId;

    return notification;
  }

  private PersistentNotification GetPersistent(Models.Notification notification) =>
    new()
    {
      Id = notification.Id,
      Type = notification.NotificationType,
      ContainedData = JsonSerializer.SerializeToUtf8Bytes(notification),
      VaultId = notification.VaultId,
      Aggregate = notification
    };
}
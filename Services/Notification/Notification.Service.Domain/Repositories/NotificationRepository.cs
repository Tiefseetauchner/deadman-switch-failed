using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.ArgumentChecks;
using DeadmanSwitchFailed.Common.Domain.Repositories;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;

public class NotificationRepository :
  CrudRepository<PersistentNotification>,
  INotificationRepository
{
  public NotificationRepository(NotificationContext context)
    : base(context.CheckNotNull(), context.Notifications)
  {
  }

  public async Task<IEnumerable<dynamic>> GetNotificationsByVaultIdAsync(Guid id) =>
    (await DbSet.AsQueryable()
      .Where(_ => _.VaultId == id)
      .ToListAsync())
    .Select(GetAggregate);

  public async Task<Guid> CreateEmailNotification(EmailNotification notification)
  {
    var result = await DbSet.AddAsync(GetPersistent(notification.CheckNotNull()));

    return result.Entity.Id;
  }

  public PersistentNotification UpdateFromAggregate(Models.Notification notification) =>
    Update(GetPersistent(notification.CheckNotNull()));

  public Task MarkNotificationAsSent(Guid id) =>
    throw new NotImplementedException();

  public async Task<dynamic> TryGetById(Guid id) =>
    await DbSet.AsQueryable()
      .SingleOrDefaultAsync(_ => _.VaultId == id) is { } result
      ? GetAggregate(result)
      : null;

  private dynamic GetAggregate(PersistentNotification persistentNotification)
  {
    var notification = persistentNotification.Type switch
    {
      NotificationType.Email => JsonSerializer.Deserialize<EmailNotification>(persistentNotification.ContainedData),
      _ => throw new ArgumentOutOfRangeException()
    };

    notification.Data.Id = persistentNotification.Id;
    notification.VaultId = persistentNotification.VaultId;

    return notification;
  }

  private PersistentNotification GetPersistent(Models.Notification notification) =>
    new()
    {
      Id = notification.Id,
      Type = notification.NotificationType,
      ContainedData = notification.NotificationType switch
      {
        NotificationType.Email => JsonSerializer.SerializeToUtf8Bytes(notification as EmailNotification),
        _ => throw new ArgumentOutOfRangeException()
      },
      VaultId = notification.VaultId,
    };
}
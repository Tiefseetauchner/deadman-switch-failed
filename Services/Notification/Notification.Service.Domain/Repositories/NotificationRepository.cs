using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Arch.EntityFrameworkCore.UnitOfWork;
using DeadmanSwitchFailed.Common.ArgumentChecks;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;

public class NotificationRepository :
  INotificationRepository,
  IDisposable
{
  private readonly NotificationContext _context;

  public NotificationRepository(NotificationContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<dynamic>> GetNotificationsByVaultIdAsync(Guid id) =>
    (await _context.Notifications.AsQueryable()
      .Where(_ => _.VaultId == id)
      .ToListAsync())
    .Select(GetAggregate);

  public async Task<Guid> CreateEmailNotification(EmailNotification notification)
  {
    var unitOfWork = new UnitOfWork<NotificationContext>(_context);

    var result = await _context.AddAsync(GetPersistent(notification.CheckNotNull()));

    await unitOfWork.SaveChangesAsync();

    return result.Entity.Id;
  }

  public Task MarkNotificationAsSent(Guid id) =>
    throw new NotImplementedException();

  public async Task<dynamic> TryGetById(Guid id) =>
    await _context.Notifications.AsQueryable()
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

    notification.Id = persistentNotification.Id;
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
      Aggregate = notification
    };

  private void Dispose(bool disposing)
  {
    if (disposing)
    {
      _context?.Dispose();
    }
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }
}
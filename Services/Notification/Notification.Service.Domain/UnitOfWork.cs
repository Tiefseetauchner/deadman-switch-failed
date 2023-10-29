using System;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain;

public class UnitOfWork : IDisposable
{
  private NotificationRepository _notificationRepository;
  private readonly NotificationContext _context;

  public UnitOfWork(NotificationContext context)
  {
    _context = context;
  }

  public INotificationRepository NotificationRepository =>
    _notificationRepository ??= new NotificationRepository(_context);

  public async Task Save() =>
    await _context.SaveChangesAsync();

  private void Dispose(bool disposing)
  {
    if (disposing)
    {
      _notificationRepository?.Dispose();
    }
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }
}
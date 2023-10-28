using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain;

public class NotificationContext : DbContext
{
  public NotificationContext(DbContextOptions<NotificationContext> dbContextOptions)
    : base(dbContextOptions)
  {
  }

  public DbSet<PersistentNotification> Notifications { get; set; }
}
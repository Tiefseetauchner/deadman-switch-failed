using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories
{
  public interface INotificationRepository
  {
    Task<IEnumerable<dynamic>> GetNotificationsByVaultIdAsync(Guid id);

    Task MarkNotificationAsSent(Guid id);

    Task<dynamic> TryGetById(Guid id);

    Task<Guid> CreateEmailNotification(Models.EmailNotification notification);
  }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories
{
  public interface INotificationRepository
  {
    Task<IEnumerable<Models.Notification>> GetNotificationsByVaultIdAsync(Guid id);

    Task MarkNotificationAsSent(Guid id);

    Task<Models.Notification> GetById(Guid id);

    Task<Guid> CreateEmail(EmailNotification notification);
  }
}
using System;
using System.Threading.Tasks;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories
{
  public interface INotificationRepository
  {
    Task<DeadmanSwitchFailed.Services.Notification.Service.Domain.Models.Notification> GetNotificationByIdAsync(Guid id);

    Task MarkNotificationAsSent(Guid id);
  }
}
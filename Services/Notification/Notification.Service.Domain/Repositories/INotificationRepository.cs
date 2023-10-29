using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.Domain.Repositories;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories
{
  public interface INotificationRepository :
    ICreateRepository<PersistentNotification>,
    IReadRepository<PersistentNotification>,
    IUpdateRepository<PersistentNotification>,
    IDeleteRepository<PersistentNotification>
  {
    Task<IEnumerable<dynamic>> GetNotificationsByVaultIdAsync(Guid id);

    Task MarkNotificationAsSent(Guid id);

    Task<Guid> CreateEmailNotification(Models.EmailNotification notification);

    PersistentNotification UpdateFromAggregate(Models.Notification notification);
  }
}
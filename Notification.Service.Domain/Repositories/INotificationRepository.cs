using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notification.Service.Domain.Models;

namespace Notification.Service.Domain.Repositories
{
  public interface INotificationRepository
  {
    Task<Models.Notification> GetNotificationByIdAsync(Guid id);

    Task MarkNotificationAsSent(Guid id);
  }
}
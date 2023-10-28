using System;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.Domain;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Models
{
  public abstract class Notification : Aggregate<PersistentNotification>
  {
    public abstract NotificationType NotificationType { get; }

    public Guid VaultId { get; set; }

    public abstract Task Send();
  }
}
using System;
using DeadmanSwitchFailed.Common.Domain;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;

public class PersistentNotification : Persistent<Notification>
{
  public PersistentNotification()
  {
  }

  public NotificationType Type { get; set; }
  public byte[] ContainedData { get; set; }
  public Guid VaultId { get; set; }
}
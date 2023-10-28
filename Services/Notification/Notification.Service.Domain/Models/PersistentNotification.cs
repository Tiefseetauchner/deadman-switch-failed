using System;
using System.ComponentModel.DataAnnotations.Schema;
using DeadmanSwitchFailed.Common.Domain;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;

public class PersistentNotification : Persistent<Notification>
{
  public NotificationType Type { get; set; }
  public byte[] ContainedData { get; set; }
  public Guid VaultId { get; set; }
  [NotMapped]
  public override Notification Aggregate { get; set; }
}
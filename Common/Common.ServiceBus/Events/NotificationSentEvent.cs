using System;

namespace DeadmanSwitchFailed.Common.ServiceBus.Events;

public class NotificationSentEvent
{
  public Guid Id { get; set; }
}
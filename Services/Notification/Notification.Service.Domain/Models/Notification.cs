using System;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Models
{
  public abstract class Notification
  {
    public Guid Id { get; private set; } = Guid.NewGuid();

    public abstract void Send();
  }

  public class EmailNotification : Notification
  {
    public string From { get; set; }
    public string To { get; set; }
    public string Cc { get; set; }
    public string Bcc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public override void Send()
    {
      throw new NotImplementedException();
    }
  }

  public enum NotificationType
  {
    Email
  }
}
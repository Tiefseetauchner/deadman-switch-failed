using System;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DeadmanSwitchFailed.Common.Domain;
using DeadmanSwitchFailed.Common.Email;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Models
{
  public abstract class Notification
  {
    public Guid Id { get; private set; } = Guid.NewGuid();
    public abstract NotificationType NotificationType { get; }

    public abstract Task Send();
  }

  [Serializable]
  public class EmailNotification : Notification
  {
    [NonSerialized] private ISmtpClientFactory _smtpClientFactory;

    public EmailNotification(ISmtpClientFactory smtpClientFactory)
    {
      _smtpClientFactory = smtpClientFactory;
    }

    public string From { get; set; }
    public string To { get; set; }
    public string Cc { get; set; }
    public string Bcc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public override NotificationType NotificationType =>
      NotificationType.Email;

    public override async Task Send() =>
      await SendEmail();

    private async Task SendEmail()
    {
      using var smtpClient = _smtpClientFactory.Create();

      await smtpClient.SendAsync(From, To, Cc, Bcc, Subject, Body);
    }
  }

  public enum NotificationType
  {
    Email = 0,
  }

  [Table("notifications")]
  public class PersistentNotification : Persistent<Notification>
  {
    public NotificationType Type { get; set; }
    public byte[] ContainedData { get; set; }
    public Guid VaultId { get; set; }
  }
}
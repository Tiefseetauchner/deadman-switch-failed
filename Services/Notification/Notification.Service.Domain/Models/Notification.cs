using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.Domain;
using DeadmanSwitchFailed.Common.Email;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Models
{
  public abstract class Notification : Aggregate<PersistentNotification>
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public abstract NotificationType NotificationType { get; }
    public Guid VaultId { get; set; }

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

    // NOTE: Only for deserialization
    public EmailNotification()
    {
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

    public override PersistentNotification Data { get; set; }
  }

  public enum NotificationType
  {
    Email = 0,
  }

  public class PersistentNotification : Persistent<Notification>
  {
    public NotificationType Type { get; set; }
    public byte[] ContainedData { get; set; }
    public Guid VaultId { get; set; }
    [NotMapped]
    public override Notification Aggregate { get; set; }
  }
}
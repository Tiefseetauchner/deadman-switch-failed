using System;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.Domain;
using DeadmanSwitchFailed.Common.Email;

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;

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

  public override IAggregate FromData(IPersistent data)
  {
    Data = data as PersistentNotification;

    return this;
  }
}
using DeadmanSwitchFailed.Common.ArgumentChecks;

namespace Notification.Service.Models
{
  public class Notification<T>
  {
    public Notification(
      Guid id)
    {
      Id = id.CheckNotNull();
    }

    public Guid Id { get; set; }
  }

  public class PersistentNotification
  {
    public PersistentNotification(
      Guid id,
      byte[] encryptedContent)
    {
      Id = id.CheckNotNull();
      EncryptedContent = encryptedContent;
    }

    public Guid Id { get; set; }
    public byte[] EncryptedContent { get; set; }
  }

  public class EmailNotification
  {
    public EmailNotification(
      string from,
      string[] to,
      string[] cc,
      string[] bcc,
      string subject,
      string body)
    {
      From = from.CheckNotNull();
      To = to.CheckNotNull();
      Cc = cc;
      Bcc = bcc;
      Subject = subject.CheckNotNull();
      Body = body.CheckNotNull();
    }

    public string From { get; }
    public string[] To { get; }
    public string[] Cc { get; }
    public string[] Bcc { get; }
    public string Subject { get; }
    public string Body { get; }
  }
}
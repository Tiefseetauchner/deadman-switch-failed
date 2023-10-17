using DeadmanSwitchFailed.Common.ArgumentChecks;

namespace Notification.Service.Models
{
  public class Notification
  {
    public Notification(
      Guid id,
      byte[] encryptedContent)
    {
      Id = id.CheckNotNull();
      EncryptedContent = encryptedContent.CheckNotNull();
    }

    public Guid Id { get; set; }
    protected byte[] EncryptedContent { get; set; }
  }

  public class EmailNotification : Notification
  {
    public EmailNotification(
      Guid id
      )
      : base(id, Array.Empty<byte>())
    {
    }
  }
}
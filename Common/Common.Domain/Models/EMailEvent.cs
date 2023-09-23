namespace DeadManSwitchFailed.Common.Domain.Models;

/// <summary>
/// Vault unlock event that sends an E-Mail
/// </summary>
public class EMailEvent : EventBase
{
  public string From { get; set; }
  public string To { get; set; }
  public string Cc { get; set; }
  public string Bcc { get; set; }
  public string Subject { get; set; }
  public string Body { get; set; }
}
namespace DeadManSwitchFailed.Common.Domain.Models;

public class EMailEvent : EventBase
{
  public string From { get; set; }
  public string To { get; set; }
  public string Cc { get; set; }
  public string Bcc { get; set; }
  public string Subject { get; set; }
  public string Body { get; set; }
}
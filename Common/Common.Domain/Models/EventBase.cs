namespace DeadManSwitchFailed.Common.Domain.Models;

/// <summary>
/// Base Class for all Events called on Vault opening
/// </summary>
public class EventBase : EntityBase
{
  public string EventToken { get; set; }
}
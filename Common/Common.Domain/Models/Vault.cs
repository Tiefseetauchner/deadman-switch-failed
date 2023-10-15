namespace DeadManSwitchFailed.Common.Domain.Models;

/// <summary>
/// The vault storing events
/// </summary>
public class Vault : EntityBase
{
  public User OwningUser { get; set; }
  public string AccessToken { get; set; }
  public string PasswordHash { get; set; }
}
namespace Domain.Models;

/// <summary>
/// The token used to authenticate an access to a vault
/// </summary>
public class EventToken : EntityBase
{
  public User OwningUser { get; set; }
  public string AccessToken { get; set; }
  public string PasswordHash { get; set; }
}
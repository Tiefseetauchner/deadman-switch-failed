namespace Domain.Models;

/// <summary>
/// A user with a vault
/// </summary>
public class User : EntityBase
{
  public string DisplayName { get; set; }
  public string UserName { get; set; }
  public string PasswordHash { get; set; }
}
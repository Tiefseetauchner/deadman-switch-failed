using DeadmanSwitchFailed.Common.Domain;
using System;

namespace DeadmanSwitchFailed.Services.Vault.Service.Domain.Models;

public class PersistentVault : Persistent<Vault>
{
  public string DisplayName { get; set; }
  public string Description { get; set; }
  public string AccessToken { get; set; }
  public string PasswordHash { get; set; }
  public Guid OwningUserId { get; set; }
}
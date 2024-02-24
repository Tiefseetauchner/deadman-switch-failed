using DeadmanSwitchFailed.Common.Domain;
using System;

namespace DeadmanSwitchFailed.Services.Vault.Service.Domain.Models;

public class Vault : Aggregate<PersistentVault>
{
  public string DisplayName { get; set; }
  public string Description { get; set; }
  public string AccessToken { get; set; }
  public string PasswordHash { get; set; }
  public Guid OwningUserId { get; set; }

  public override IAggregate FromData(IPersistent data)
  {
    Data = data as PersistentVault;

    return this;
  }
}
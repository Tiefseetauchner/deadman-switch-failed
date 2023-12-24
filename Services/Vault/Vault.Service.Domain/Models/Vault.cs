using DeadmanSwitchFailed.Common.Domain;

namespace DeadmanSwitchFailed.Services.Vault.Service.Domain.Models;

public class Vault : Aggregate<PersistentVault>
{
  public override IAggregate FromData(IPersistent data) =>
    throw new System.NotImplementedException();
}
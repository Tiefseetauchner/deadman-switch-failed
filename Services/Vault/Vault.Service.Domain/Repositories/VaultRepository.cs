using DeadmanSwitchFailed.Common.ArgumentChecks;
using DeadmanSwitchFailed.Common.Domain.Repositories;
using DeadmanSwitchFailed.Services.Vault.Service.Domain.Models;

namespace DeadmanSwitchFailed.Services.Vault.Service.Domain.Repositories;

public class VaultRepository : CrudRepository<PersistentVault>, IVaultRepository
{
  public VaultRepository(VaultContext context)
    : base(context.CheckNotNull(), context.Vaults)
  {
  }
}

public interface IVaultRepository :
  ICreateRepository<PersistentVault>,
  IReadRepository<PersistentVault>,
  IUpdateRepository<PersistentVault>,
  IDeleteRepository<PersistentVault>
{
}
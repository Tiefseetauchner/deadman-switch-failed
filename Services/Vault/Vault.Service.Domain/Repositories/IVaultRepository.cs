using DeadmanSwitchFailed.Common.Domain.Repositories;
using DeadmanSwitchFailed.Services.Vault.Service.Domain.Models;

namespace DeadmanSwitchFailed.Services.Vault.Service.Domain.Repositories;

public interface IVaultRepository :
  ICreateRepository<PersistentVault>,
  IReadRepository<PersistentVault>,
  IUpdateRepository<PersistentVault>,
  IDeleteRepository<PersistentVault>
{
}
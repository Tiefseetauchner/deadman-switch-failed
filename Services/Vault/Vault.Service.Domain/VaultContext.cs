using DeadmanSwitchFailed.Services.Vault.Service.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadmanSwitchFailed.Services.Vault.Service.Domain;

public class VaultContext : DbContext
{
  public VaultContext(DbContextOptions<VaultContext> dbContextOptions)
    : base(dbContextOptions)
  {
  }

  public DbSet<PersistentVault> Vaults { get; set; }
}
using System;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Services.Vault.Service.Domain.Repositories;

namespace DeadmanSwitchFailed.Services.Vault.Service.Domain;

public class UnitOfWork : IDisposable
{
  private VaultRepository _notificationRepository;
  private readonly VaultContext _context;

  public UnitOfWork(VaultContext context)
  {
    _context = context;
  }

  public IVaultRepository VaultRepository =>
    _notificationRepository = new VaultRepository(_context);

  public async Task Save() =>
    await _context.SaveChangesAsync();

  private void Dispose(bool disposing)
  {
    if (disposing)
    {
      _notificationRepository?.Dispose();
    }
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }
}
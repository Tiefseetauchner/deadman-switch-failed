using Microsoft.EntityFrameworkCore;

namespace DeadmanSwitchFailed.Common.Domain.Repositories;

public abstract class RepositoryBase<T> where T : class
{
  readonly protected DbContext DbContext;
  readonly protected DbSet<T> DbSet;

  protected RepositoryBase(DbContext dbContext, DbSet<T> dbSet)
  {
    DbContext = dbContext;
    DbSet = dbSet;
  }
}
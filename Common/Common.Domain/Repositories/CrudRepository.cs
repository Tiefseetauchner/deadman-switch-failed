using System;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.ArgumentChecks;
using Microsoft.EntityFrameworkCore;

namespace DeadmanSwitchFailed.Common.Domain.Repositories;

public class CrudRepository<T> : RepositoryBase<T>,
  ICreateRepository<T>,
  IReadRepository<T>,
  IUpdateRepository<T>,
  IDeleteRepository<T>
  where T : class
{
  public CrudRepository(DbContext dbContext, DbSet<T> dbSet)
    : base(dbContext, dbSet)
  {
  }

  public T Create(T entity) =>
    DbSet.Add(entity).Entity;

  public T CreateFromAggregate<TAggregate>(TAggregate aggregate, Func<TAggregate, T> entityFactory) =>
    DbSet.Add(entityFactory.CheckNotNull()(aggregate)).Entity;

  public async Task<T> CreateAsync(T entity) =>
    (await DbSet.AddAsync(entity)).Entity;

  public T GetById(Guid id) =>
    DbSet.Find(id);

  public async Task<T> GetByIdAsync(Guid id) =>
    await DbSet.FindAsync(id);

  public T Update(T entity) =>
    DbSet.Update(entity).Entity;

  public void Delete(T entity) =>
    DbSet.Remove(entity);

  public void DeleteById(Guid id) =>
    DbSet.Remove(GetById(id));
}
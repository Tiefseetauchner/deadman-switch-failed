using System;
using System.Threading.Tasks;

namespace DeadmanSwitchFailed.Common.Domain.Repositories;

public interface ICreateRepository<T> : IRepositoryBase
{
  T Create(T entity);

  T CreateFromAggregate<TAggregate>(TAggregate aggregate, Func<TAggregate, T> entityFactory);

  Task<T> CreateAsync(T entity);

  Task<T> CreateFromAggregateAsync<TAggregate>(TAggregate aggregate, Func<TAggregate, T> entityFactory);
}
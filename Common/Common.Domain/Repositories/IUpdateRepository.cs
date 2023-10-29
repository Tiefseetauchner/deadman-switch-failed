using System;

namespace DeadmanSwitchFailed.Common.Domain.Repositories;

public interface IUpdateRepository<T> : IRepositoryBase
{
  T Update(T entity);

  T UpdateFromAggregate<TAggregate>(TAggregate aggregate, Func<TAggregate, T> persistentFactory);
}
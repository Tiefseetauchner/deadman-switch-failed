using System;
using System.Threading.Tasks;

namespace DeadmanSwitchFailed.Common.Domain.Repositories;

public interface IReadRepository<T>
{
  T GetById(Guid id);

  Task<T> GetByIdAsync(Guid id);
}
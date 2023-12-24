using System;

namespace DeadmanSwitchFailed.Common.Domain;

public abstract class Aggregate<T> : IAggregate
  where T : IPersistent
{
  protected Aggregate(T data)
  {
    Data = data;
  }

  protected Aggregate()
  {
  }

  [NonSerialized] public T Data;

  public Guid Id => Data.Id;

  public abstract IAggregate FromData(IPersistent data);
}
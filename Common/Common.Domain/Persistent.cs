using System;

namespace DeadmanSwitchFailed.Common.Domain;

public abstract class Persistent<T> : IPersistent
  where T : IAggregate
{
  protected Persistent()
  {
  }

  public Guid Id { get; set; }
}
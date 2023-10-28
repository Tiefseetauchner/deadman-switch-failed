using System;

namespace DeadmanSwitchFailed.Common.Domain;

public abstract class Persistent<T>
{
  public Guid Id { get; set; }
  public abstract T Aggregate { get; set; }
}
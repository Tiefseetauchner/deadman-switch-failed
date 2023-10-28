using System;

namespace DeadmanSwitchFailed.Common.Domain;

public abstract class Aggregate<T>
{
  [NonSerialized] public Guid Id;

  [NonSerialized] public T Data;
}
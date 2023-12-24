using System;

namespace DeadmanSwitchFailed.Common.Domain;

public interface IAggregate
{
  Guid Id { get; }

  IAggregate FromData(IPersistent data);
}
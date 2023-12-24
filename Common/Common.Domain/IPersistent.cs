using System;

namespace DeadmanSwitchFailed.Common.Domain;

public interface IPersistent
{
  public Guid Id { get; set; }
}
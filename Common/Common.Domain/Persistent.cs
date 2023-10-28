using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeadmanSwitchFailed.Common.Domain;

public abstract class Persistent<T>
{
  public Guid Id { get; set; }

  [NotMapped]
  public abstract T Aggregate { get; set; }
}
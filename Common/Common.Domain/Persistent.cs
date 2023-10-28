using System;
using Dapper.Contrib.Extensions;

namespace DeadmanSwitchFailed.Common.Domain;

public abstract class Persistent<T>
{
  [Key]
  public Guid Id { get; set; }
}
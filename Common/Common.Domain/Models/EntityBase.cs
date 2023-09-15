using System;
using Dapper.Contrib.Extensions;

namespace DeadManSwitchFailed.Common.Domain.Models;

/// <summary>
/// Base Class for all database entities
/// </summary>
public abstract class EntityBase
{
  [Key]
  public Guid Id { get; set; }
}
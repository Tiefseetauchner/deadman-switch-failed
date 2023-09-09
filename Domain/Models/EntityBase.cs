using Dapper.Contrib.Extensions;

namespace Domain.Models;

/// <summary>
/// Base Class for all database entities
/// </summary>
public abstract class EntityBase
{
  [Key]
  public Guid Id { get; set; }
}
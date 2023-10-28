namespace DeadmanSwitchFailed.Common.Domain;

public abstract class Aggregate<T>
{
  public abstract T Data { get; set; }
}
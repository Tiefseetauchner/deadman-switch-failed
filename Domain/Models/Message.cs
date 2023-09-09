namespace Domain.Models;

/// <summary>
/// Message to be displayed after unlocking vault
/// </summary>
public class Message : EntityBase
{
  public string Addressee { get; set; }
  public string Content { get; set; }
  public EventToken AccessingEvent { get; set; }
}
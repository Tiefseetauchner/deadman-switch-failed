namespace DeadManSwitchFailed.Service.Configuration;

public class MailServerConfiguration
{
  public string Host { get; set; }
  public int Port { get; set; }
  public int Timeout { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
}
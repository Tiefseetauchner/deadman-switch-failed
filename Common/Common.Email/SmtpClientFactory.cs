using System.Net;
using System.Net.Mail;
using DeadmanSwitchFailed.Common.ArgumentChecks;

namespace DeadmanSwitchFailed.Common.Email;

public class SmtpClientFactory : ISmtpClientFactory
{
  private readonly string _host;
  private readonly int _port;
  private readonly int _timeout;
  private readonly string _username;
  private readonly string _password;

  public SmtpClientFactory(
    string host,
    int port,
    int timeout,
    string username,
    string password)
  {
    _host = host.CheckNotNull();
    _port = port.CheckNotNull();
    _timeout = timeout.CheckNotNull();
    _username = username.CheckNotNull();
    _password = password.CheckNotNull();
  }

  public SmtpClient Create() =>
    new()
    {
      Credentials = new NetworkCredential(_username, _password),
      Host = _host,
      Port = _port,
      Timeout = _timeout,
    };
}

public interface ISmtpClientFactory
{
  SmtpClient Create();
}
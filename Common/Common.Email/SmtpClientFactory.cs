using DeadmanSwitchFailed.Common.ArgumentChecks;

namespace DeadmanSwitchFailed.Common.Email;

public class SmtpClientFactory : ISmtpClientFactory
{
  private readonly string _host;
  private readonly int _port;
  private readonly int _timeout;
  private readonly string _username;
  private readonly string _password;

  /// <param name="timeout">Timeout in Milliseconds</param>
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
    _username = username;
    _password = password;
  }

  public ISmtpClient Create()
  {
    var mailKitClient = new MailKit.Net.Smtp.SmtpClient();
    mailKitClient.Timeout = _timeout;

    return new SmtpClient(mailKitClient,
      _host,
      _port,
      _username,
      _password);
  }
}
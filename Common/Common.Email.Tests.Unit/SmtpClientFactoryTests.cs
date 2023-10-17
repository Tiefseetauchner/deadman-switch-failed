using DeadmanSwitchFailed.Common.Email;

namespace Common.Email.Tests.Unit;

public class SmtpClientFactoryTests
{
  [Test]
  public void Create()
  {
    var factory = new SmtpClientFactory("localhost", 25, 1000, "Username", "Password");

    using var smtpClient = factory.Create();

    Assert.That(smtpClient, Is.TypeOf<SmtpClient>());
    var castSmtpClient = smtpClient as SmtpClient;

    Assert.Multiple(() =>
    {
      Assert.That(castSmtpClient.Client.IsConnected, Is.False);
      Assert.That(castSmtpClient.Client.IsAuthenticated, Is.False);
      Assert.That(castSmtpClient.Client.Timeout, Is.EqualTo(1000));
      Assert.That(castSmtpClient.Username, Is.EqualTo("Username"));
      Assert.That(castSmtpClient.Password, Is.EqualTo("Password"));
      Assert.That(castSmtpClient.Host, Is.EqualTo("localhost"));
    });
  }
}
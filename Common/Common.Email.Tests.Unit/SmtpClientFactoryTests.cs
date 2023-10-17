using System.Net;
using DeadmanSwitchFailed.Common.Email;

namespace Common.Email.Tests.Unit;

public class SmtpClientFactoryTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void Create()
  {
    var factory = new SmtpClientFactory("host", 200, 10, "username", "password");

    var smtpClient = factory.Create();

    Assert.That(smtpClient.Host, Is.EqualTo("host"));
    Assert.That(smtpClient.Port, Is.EqualTo(200));
    Assert.That(smtpClient.Timeout, Is.EqualTo(10));
    Assert.That(smtpClient.Credentials, Is.TypeOf<NetworkCredential>());
  }
}
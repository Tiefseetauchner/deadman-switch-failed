using DeadManSwitchFailed.Common.Domain.Models;
using NUnit.Framework;

namespace Common.Encryption.Tests.Unit;

[TestFixture]
public class EncryptedTest
{
  [Test]
  public void Decrypt()
  {
    var message = new EventToken();

    var encrpyted = Encrypted<EventToken>.Create(message, "Password");

    var decrypted = encrpyted.Decrypt("Password");

    Assert.That(decrypted, Is.EqualTo(message));
  }
}
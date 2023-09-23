using System;
using DeadManSwitchFailed.Common.Domain.Models;
using NUnit.Framework;

namespace Common.Encryption.Tests.Unit;

[TestFixture]
public class EncryptedTest
{
  [Test]
  public void Decrypt()
  {
    var message = new EventToken()
    {
      Id = Guid.NewGuid(),
      AccessToken = "asdf",
      OwningUser = new User
      {
        Id = Guid.NewGuid(),
        DisplayName = "asdf",
        PasswordHash = "aaaaaadfioje",
        UserName = "asdfoht4ewdf803rwoijef"
      },
      PasswordHash = "aaaaaaaaaaaaaaaaaaaaaaa"
    };

    var encrpyted = Encrypted<EventToken>.Create(message, "asdfghjkl");

    var decrypted = encrpyted.Decrypt("asdfghjkl");

    AssertEventTokenEquals(decrypted, message);
  }

  private void AssertEventTokenEquals(EventToken token, EventToken expected)
  {
    Assert.That(token.AccessToken, Is.EqualTo(expected.AccessToken));
    Assert.That(token.Id, Is.EqualTo(expected.Id));
    Assert.That(token.PasswordHash, Is.EqualTo(expected.PasswordHash));
    AssertUserEquals(token.OwningUser, expected.OwningUser);
  }

  private void AssertUserEquals(User user, User expected)
  {
    Assert.That(user.UserName, Is.EqualTo(expected.UserName));
    Assert.That(user.PasswordHash, Is.EqualTo(expected.PasswordHash));
    Assert.That(user.DisplayName, Is.EqualTo(expected.DisplayName));
    Assert.That(user.Id, Is.EqualTo(expected.Id));
  }
}
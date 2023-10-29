using System;
using NUnit.Framework;

namespace Common.Encryption.Tests.Unit;

[TestFixture]
public class EncryptedTest
{
  [Test]
  public void Decrypt()
  {
    var testObject = new TestClass(
      Guid.NewGuid(),
      "name",
      new TestSubClass(
        Guid.NewGuid(),
        "name"));

    var encrpyted = Encrypted<TestClass>.Create(testObject, "password");

    var decrypted = encrpyted.Decrypt("password");

    AssertTestClassEquals(decrypted, testObject);
  }

  private void AssertTestClassEquals(TestClass actual, TestClass expected)
  {
    Assert.That(actual.Id, Is.EqualTo(expected.Id));
    Assert.That(actual.Name, Is.EqualTo(expected.Name));
    AssertUserEquals(actual.TestSubClass, expected.TestSubClass);
  }

  private void AssertUserEquals(TestSubClass actual, TestSubClass expected)
  {
    Assert.That(actual.Id, Is.EqualTo(expected.Id));
    Assert.That(actual.Name, Is.EqualTo(expected.Name));
  }

  private record TestClass(
    Guid Id,
    string Name,
    TestSubClass TestSubClass);

  private record TestSubClass(
    Guid Id,
    string Name);
}
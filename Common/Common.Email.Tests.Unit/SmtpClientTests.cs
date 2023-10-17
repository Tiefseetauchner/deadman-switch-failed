using DeadmanSwitchFailed.Common.Email;
using MimeKit;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Common.Email.Tests.Unit;

public class SmtpClientTests
{
  [Test]
  public async Task ConnectAsync()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, "Username", "Password");

    await client.Connect();

    mailKitClient.Verify(_ => _.ConnectAsync("Host", 42, MailKit.Security.SecureSocketOptions.Auto, default));
    mailKitClient.Verify(_ => _.AuthenticateAsync(It.Is<NetworkCredential>(_ =>
      _.UserName == "Username"
      && _.Password == "Password"), default));
  }

  [Test]
  public async Task ConnectAsync_NoCredentials()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    await client.Connect();

    mailKitClient.Verify(_ => _.ConnectAsync("Host", 42, MailKit.Security.SecureSocketOptions.Auto, default));
    mailKitClient.VerifyNoOtherCalls();
  }

  [Test]
  public async Task SendAsync()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    await client.SendAsync("from@test.com", "to@test.com", "cc@test.com", "bcc@test.com", "subject", "body");

    mailKitClient.Verify(_ => _.SendAsync(It.Is<MimeMessage>(_ =>
      _.From[0].ToString() == "from@test.com" &&
      _.To[0].ToString() == "to@test.com" &&
      _.Cc[0].ToString() == "cc@test.com" &&
      _.Bcc[0].ToString() == "bcc@test.com" &&
      _.Subject == "subject" &&
      _.TextBody == "body"), default, null));
    mailKitClient.VerifyNoOtherCalls();
  }

  [Test]
  public async Task SendAsync_MultipleAddresses()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    var toAddresses = new[] { "to1@test.com", "to2@test.com" };
    var ccAddresses = new[] { "cc1@test.com", "cc2@test.com" };
    var bccAddresses = new[] { "bcc1@test.com", "bcc2@test.com" };
    await client.SendAsync("from@test.com", toAddresses, ccAddresses, bccAddresses, "subject", "body");

    mailKitClient.Verify(_ => _.SendAsync(It.Is<MimeMessage>(_ =>
      _.From[0].ToString() == "from@test.com" &&
      _.To[0].ToString() == "to1@test.com" &&
      _.To[1].ToString() == "to2@test.com" &&
      _.Cc[0].ToString() == "cc1@test.com" &&
      _.Cc[1].ToString() == "cc2@test.com" &&
      _.Bcc[0].ToString() == "bcc1@test.com" &&
      _.Bcc[1].ToString() == "bcc2@test.com" &&
      _.Subject == "subject" &&
      _.TextBody == "body"), default, null));
    mailKitClient.VerifyNoOtherCalls();
  }

  [Test]
  public async Task SendAsync_MultipleAddresses_CommaDelimited()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    await client.SendAsync("from@test.com",
      "to1@test.com,to2@test.com",
      "cc1@test.com,cc2@test.com",
      "bcc1@test.com,bcc2@test.com",
      "subject",
      "body");

    mailKitClient.Verify(_ => _.SendAsync(It.Is<MimeMessage>(_ =>
      _.From[0].ToString() == "from@test.com" &&
      _.To[0].ToString() == "to1@test.com" &&
      _.To[1].ToString() == "to2@test.com" &&
      _.Cc[0].ToString() == "cc1@test.com" &&
      _.Cc[1].ToString() == "cc2@test.com" &&
      _.Bcc[0].ToString() == "bcc1@test.com" &&
      _.Bcc[1].ToString() == "bcc2@test.com" &&
      _.Subject == "subject" &&
      _.TextBody == "body"), default, null));
    mailKitClient.VerifyNoOtherCalls();
  }

  [Test]
  public void SendAsync_InvalidFrom()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    var exception = Assert.ThrowsAsync<FormatException>(async () => await client.SendAsync("notAnEmail",
      "to1@test.com",
      "cc1@test.com",
      "bcc1@test.com",
      "subject",
      "body"));

    Assert.That(exception.Message, Is.EqualTo("The specified string is not in the form required for an e-mail address."));
  }

  [Test]
  public void SendAsync_InvalidTo()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    var exception = Assert.ThrowsAsync<FormatException>(async () => await client.SendAsync("from@test.com",
      "notAnEmail",
      "cc1@test.com",
      "bcc1@test.com",
      "subject",
      "body"));

    Assert.That(exception.Message, Is.EqualTo("The specified string is not in the form required for an e-mail address."));
  }

  [Test]
  public void SendAsync_InvalidCc()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    var exception = Assert.ThrowsAsync<FormatException>(async () => await client.SendAsync("from@test.com",
      "to1@test.com",
      "notAnEmail",
      "bcc1@test.com",
      "subject",
      "body"));

    Assert.That(exception.Message, Is.EqualTo("The specified string is not in the form required for an e-mail address."));
  }

  [Test]
  public void SendAsync_InvalidBcc()
  {
    var mailKitClient = new Mock<MailKit.Net.Smtp.ISmtpClient>();
    var client = new SmtpClient(mailKitClient.Object, "Host", 42, null, null);

    var exception = Assert.ThrowsAsync<FormatException>(async () => await client.SendAsync("from@test.com",
      "to1@test.com",
      "cc1@test.com",
      "notAnEmail",
      "subject",
      "body"));

    Assert.That(exception.Message, Is.EqualTo("The specified string is not in the form required for an e-mail address."));
  }
}
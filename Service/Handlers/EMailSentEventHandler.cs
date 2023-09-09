using System.Data;
using System.Net;
using System.Net.Mail;
using Dapper.Contrib.Extensions;
using Domain.Models;
using Rebus.Bus;
using Rebus.Handlers;
using ServiceBus.Events;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Service.Handlers;

public class EMailSentEventHandler : IHandleMessages<SendEMailEvent>
{
  private readonly IBus _bus;
  private readonly IDbConnection _connection;

  public EMailSentEventHandler(IBus bus, IDbConnection connection)
  {
    _bus = bus;
    _connection = connection;
  }

  public async Task Handle(SendEMailEvent message)
  {
    Console.WriteLine("asdf");

    var emailData = _connection.Get<EMailEvent>(message.Id);

    var mailMessage = new MailMessage();
    mailMessage.To.Add(emailData.To);
    mailMessage.CC.Add(emailData.Cc);
    mailMessage.Bcc.Add(emailData.Bcc);
    mailMessage.Subject = emailData.Subject;
    // TODO somehow transform?
    mailMessage.Body = emailData.Body;

    using var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["MailServer.Host"]);

    smtpClient.Credentials = new NetworkCredential(
      ConfigurationManager.AppSettings["MailServer.UserName"],
      ConfigurationManager.AppSettings["MailServer.Password"]);

    await smtpClient.SendMailAsync(mailMessage);

    await _bus.Publish(new EMailSentEvent { Id = message.Id });
  }
}
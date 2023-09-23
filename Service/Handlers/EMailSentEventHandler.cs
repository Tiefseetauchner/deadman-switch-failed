using System.Data;
using System.Net.Mail;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DeadManSwitchFailed.Common.ArgumentChecks;
using DeadManSwitchFailed.Common.Domain.Models;
using DeadManSwitchFailed.Common.Email;
using DeadManSwitchFailed.Common.ServiceBus.Events;
using Rebus.Bus;
using Rebus.Handlers;

namespace DeadManSwitchFailed.Service.Handlers;

public class EMailSentEventHandler : IHandleMessages<SendEMailEvent>
{
  private readonly ISmtpClientFactory _smtpClientFactory;
  private readonly IBus _bus;
  private readonly IDbConnection _connection;

  public EMailSentEventHandler(
    IBus bus,
    IDbConnection connection,
    ISmtpClientFactory smtpClientFactory)
  {
    _smtpClientFactory = smtpClientFactory.CheckNotNull();
    _bus = bus.CheckNotNull();
    _connection = connection.CheckNotNull();
  }

  public async Task Handle(SendEMailEvent message)
  {
    message.CheckNotNull();

    var emailData = _connection.Get<EMailEvent>(message.Id);

    var mailMessage = new MailMessage();
    mailMessage.To.Add(emailData.To);
    mailMessage.CC.Add(emailData.Cc);
    mailMessage.Bcc.Add(emailData.Bcc);
    mailMessage.Subject = emailData.Subject;
    // TODO somehow transform? => put into Common.Email (or call DocumentPartner ;) )
    mailMessage.Body = emailData.Body;

    using var smtpClient = _smtpClientFactory.Create();

    await smtpClient.SendMailAsync(mailMessage);

    await _bus.Publish(new EMailSentEvent { Id = message.Id });
  }
}
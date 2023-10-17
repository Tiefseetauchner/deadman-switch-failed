using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeadmanSwitchFailed.Common.Email
{
  public interface ISmtpClient : IDisposable

  {
    Task SendAsync(
      string from,
      string to,
      string cc,
      string bcc,
      string subject,
      string body,
      CancellationToken cancellationToken = default);

    Task SendAsync(
      string from,
      string[] to,
      string[] cc,
      string[] bcc,
      string subject,
      string body,
      CancellationToken cancellationToken = default);

    Task Connect();
  }
}
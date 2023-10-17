using DeadmanSwitchFailed.Common.ArgumentChecks;
using MimeKit;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace DeadmanSwitchFailed.Common.Email
{
  public class SmtpClient : ISmtpClient, IDisposable
  {
    private bool disposedValue;

    public MailKit.Net.Smtp.ISmtpClient Client { get; }
    public string Host { get; }
    public int Port { get; }
    public string Username { get; }
    public string Password { get; }

    public SmtpClient(
      MailKit.Net.Smtp.ISmtpClient client,
      string host,
      int port,
      string username,
      string password)
    {
      Client = client.CheckNotNull();
      Host = host.CheckNotNull();
      Port = port.CheckNotNull();
      Username = username;
      Password = password;
    }

    public async Task Connect()
    {
      await Client.ConnectAsync(Host, Port);

      if (Username != null && Password != null)
        await Client.AuthenticateAsync(new NetworkCredential(Username, Password));
    }

    /// <summary>
    /// Sends an Email
    /// </summary>
    /// <param name="from">Comma Delimited Email List</param>
    /// <param name="to">Comma Delimited Email List</param>
    /// <param name="cc">Comma Delimited Email List</param>
    /// <param name="bcc">Comma Delimited Email List</param>
    /// <param name="subject">Single Line Subject</param>
    /// <param name="body">Plain Text Body</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SendAsync(
      string from,
      string[] to,
      string[] cc,
      string[] bcc,
      string subject,
      string body,
      CancellationToken cancellationToken = default) =>
      await SendAsync(from, string.Join(',', to), string.Join(',', cc), string.Join(',', bcc), subject, body, cancellationToken);

    /// <summary>
    /// Sends an Email
    /// </summary>
    /// <param name="from">Comma Delimited Email List</param>
    /// <param name="to">Comma Delimited Email List</param>
    /// <param name="cc">Comma Delimited Email List</param>
    /// <param name="bcc">Comma Delimited Email List</param>
    /// <param name="subject">Single Line Subject</param>
    /// <param name="body">Plain Text Body</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SendAsync(
      string from,
      string to,
      string cc,
      string bcc,
      string subject,
      string body,
      CancellationToken cancellationToken = default)
    {
      using var mailMessage = new MailMessage(from, to, subject, body);

      if (!string.IsNullOrWhiteSpace(cc))
        mailMessage.CC.Add(cc);

      if (!string.IsNullOrWhiteSpace(bcc))
        mailMessage.Bcc.Add(bcc);

      // TODO (lena.tauchner): escape source and allow rich text editing
      mailMessage.IsBodyHtml = false;

      await Client.SendAsync(MimeMessage.CreateFromMailMessage(mailMessage), cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          Client.Dispose();
        }

        disposedValue = true;
      }
    }

    public void Dispose()
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
namespace DeadmanSwitchFailed.Common.Email;

public interface ISmtpClientFactory
{
  ISmtpClient Create();
}
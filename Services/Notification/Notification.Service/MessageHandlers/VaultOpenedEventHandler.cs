﻿using System.Threading.Tasks;
using DeadmanSwitchFailed.Common.ArgumentChecks;
using DeadmanSwitchFailed.Common.Email;
using DeadmanSwitchFailed.Common.ServiceBus.Events;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace DeadmanSwitchFailed.Services.Notification.Service.MessageHandlers
{
  public class VaultOpenedEventHandler : IHandleMessages<VaultOpenedEvent>
  {
    private readonly INotificationRepository _notificationRepository;
    private readonly ISmtpClientFactory _smtpClientFactory;
    private readonly ILogger _logger;

    public VaultOpenedEventHandler(INotificationRepository notificationRepository, ISmtpClientFactory smtpClientFactory, ILogger logger)
    {
      _notificationRepository = notificationRepository;
      _smtpClientFactory = smtpClientFactory;
      _logger = logger;
    }

    public async Task Handle(VaultOpenedEvent message)
    {
      message.CheckNotNull();

      var notifications = await _notificationRepository.GetNotificationsByVaultIdAsync(message.Id);

      if (notifications == null)
      {
        _logger.LogWarning($"Notification with id {message.Id} was not found.");

        return;
      }

      foreach (var notification in notifications)
      {
        await notification.Send();
        await _notificationRepository.MarkNotificationAsSent(notification.Id);
      }
    }
  }
}
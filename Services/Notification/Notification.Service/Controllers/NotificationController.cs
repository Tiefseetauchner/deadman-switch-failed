using System;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeadmanSwitchFailed.Services.Notification.Service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly INotificationRepository _notificationRepository;

    public NotificationController(INotificationRepository notificationRepository)
    {
      _notificationRepository = notificationRepository;
    }

    [HttpGet("")]
    public ActionResult<EmailNotification> Get_Notifications()
    {
      return Ok(_notificationRepository.GetNotificationsByVaultIdAsync(new Guid()));
    }

    [HttpPost("")]
    public ActionResult<Guid> Create_EmailNotification(EmailNotification notification)
    {
      return Ok(_notificationRepository.CreateEmailNotification(notification));
    }
  }
}
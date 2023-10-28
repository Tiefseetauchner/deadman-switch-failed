using System;
using System.Threading.Tasks;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmailNotification>> Get_Notifications(Guid id)
    {
      return await _notificationRepository.TryGetById(id) is { } notification ? Ok(notification) : NotFound();
    }

    [HttpGet("vault/{vaultId:guid}")]
    public async Task<ActionResult<dynamic>> Get_Notifications_FromVault(Guid vaultId)
    {
      return Ok(await _notificationRepository.GetNotificationsByVaultIdAsync(vaultId));
    }

    [HttpPost("")]
    public async Task<ActionResult<Guid>> Create_EmailNotification(EmailNotification notification)
    {
      return Ok(await _notificationRepository.CreateEmailNotification(notification));
    }
  }
}
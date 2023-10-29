using System;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Services.Notification.Service.Domain;
using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeadmanSwitchFailed.Services.Notification.Service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly UnitOfWork _unitOfWork;

    public NotificationController(UnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmailNotification>> Get_Notification(Guid id)
    {
      return await _unitOfWork.NotificationRepository.GetByIdAsync(id) is { } notification ? Ok(notification) : NotFound();
    }

    [HttpGet("vault/{vaultId:guid}")]
    public async Task<ActionResult<dynamic>> Get_Notifications_FromVault(Guid vaultId)
    {
      return Ok(await _unitOfWork.NotificationRepository.GetNotificationsByVaultIdAsync(vaultId));
    }

    [HttpPost("")]
    public async Task<ActionResult<Guid>> Create_EmailNotification(EmailNotification notification)
    {
      var emailNotification = await _unitOfWork.NotificationRepository.CreateEmailNotification(notification);

      await _unitOfWork.Save();

      return Ok(emailNotification);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> Update_Email(Guid id, EmailNotification notification)
    {
      var updateFromAggregate = _unitOfWork.NotificationRepository.UpdateFromAggregate(notification);

      await _unitOfWork.Save();

      return Ok(updateFromAggregate);
    }
  }
}
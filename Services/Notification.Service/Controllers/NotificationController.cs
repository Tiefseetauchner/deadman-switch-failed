using DeadmanSwitchFailed.Services.Notification.Service.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeadmanSwitchFailed.Services.Notification.Service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    [HttpGet("")]
    public ActionResult<EmailNotification> Get_EmailNotification()
    {
      return Ok();
    }
  }
}
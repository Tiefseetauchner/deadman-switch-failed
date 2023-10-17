using Microsoft.AspNetCore.Mvc;
using Notification.Service.Models;

namespace Notification.Service.Controllers
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
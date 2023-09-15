using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DeadManSwitchFailed.Common.Domain.Models;
using DeadManSwitchFailed.Common.ServiceBus.Events;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;

namespace DeadManSwitchFailed.Web.Controllers;

[Route("email")]
public class SendEMailTestController : Controller
{
  private readonly IBus _bus;
  private readonly IDbConnection _connection;

  public SendEMailTestController(
    IBus bus,
    IDbConnection connection)
  {
    _bus = bus;
    _connection = connection;
  }

  [HttpPost("{id:guid}")]
  public async Task Send(Guid id) =>
    await _bus.Publish(new SendEMailEvent { Id = id });

  [HttpGet]
  public IActionResult GetAll() =>
    Ok(_connection.GetAll<EMailEvent>());
  //
  // [HttpPost("{id:guid}")]
  // public void Create(Guid id) =>
  //   _connection.Insert(new EMailEvent());
}
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using ServiceBus.Events;

namespace Web.Controllers;

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

  [HttpGet("{id:guid}")]
  public async Task Send(Guid id) =>
    await _bus.Publish(new SendEMailEvent { Id = id });

  [HttpGet]
  public IActionResult GetAll() =>
    Ok(_connection.GetAll<EMailEvent>());

  [HttpPost("{id:guid}")]
  public void Create(Guid id) =>
    _connection.Insert(new EMailEvent());
}
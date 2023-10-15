using System;
using System.Data;
using System.Net;
using Dapper.Contrib.Extensions;
using DeadManSwitchFailed.Common.ArgumentChecks;
using DeadManSwitchFailed.Common.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeadManSwitchFailed.Web.Controllers;

[Route("users")]
public class UserController : Controller
{
  private readonly IDbConnection _dbConnection;

  public UserController(
    IDbConnection dbConnection)
  {
    _dbConnection = dbConnection;
  }
  
  [HttpGet("{id}")]
  public IActionResult GetUser(Guid id)
  {
    id.CheckNotNull();
    
    var user = _dbConnection.Get<User>(id);
    
    return Ok(user);
  }
  
  [HttpPost]
  [ProducesResponseType(typeof(User), (int)HttpStatusCode.Created)]
  public IActionResult CreateUser([FromBody]UserCreateData user)
  {
    user.CheckNotNull();

    var createdUser = new User
    {
      Id = Guid.NewGuid(),
      DisplayName = user.DisplayName,
      UserName = user.UserName,
      PasswordHash = user.PasswordHash,
    };
    
    _dbConnection.Insert(createdUser);
    
    return Ok(createdUser);
  }
}

public class UserCreateData
{
  public string DisplayName { get; set; }
  public string UserName { get; set; }
  public string PasswordHash { get; set; }
}
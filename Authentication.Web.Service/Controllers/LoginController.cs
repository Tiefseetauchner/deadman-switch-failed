using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using Dapper.Contrib.Extensions;
using DeadmanSwitchFailed.Authentication.Web.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeadmanSwitchFailed.Authentication.Web.Service.Controllers;

[Controller]
[Route("[controller]")]
public class LoginController
{
  private readonly IDbConnection _connection;

  public LoginController(IDbConnection connection)
  {
    _connection = connection;
  }

  [HttpGet]
  [Route("{loginCode}")]
  public SessionToken SessionToken(string loginCode)
  {
    var token = RandomNumberGenerator.GetBytes(64);
    var sessionToken = new SessionToken(Convert.ToBase64String(token));

    var loginToken = LoginToken.Parse(loginCode, sessionToken);

    _connection.Insert(loginToken);

    return sessionToken;
  }
}
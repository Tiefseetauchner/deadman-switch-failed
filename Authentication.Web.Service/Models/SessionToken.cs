using Dapper.Contrib.Extensions;

namespace DeadmanSwitchFailed.Authentication.Web.Service.Models;

[System.ComponentModel.DataAnnotations.Schema.Table("SessionToken")]
public class SessionToken
{
  [Key]
  public string Token { get; set; }

  public SessionToken(string token)
  {
    Token = token;
  }
}
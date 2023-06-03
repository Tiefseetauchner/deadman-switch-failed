using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace DeadmanSwitchFailed.Authentication.Web.Service.Models;

[Table("LoginToken")]
public class LoginToken
{
  public string Username { get; set; }
  public string Userid { get; set; }
  public string Key { get; set; }
  [Key]
  public SessionToken SessionToken { get; set; }

  private readonly static Regex s_tokenCodeParseRegex = new(@"(?<userName>[\p{L}.]+):(?<userId>\d+):(?<key>[a-zA-Z\d\/+]+==)");

  public LoginToken(string username, string userid, string key, SessionToken sessionToken)
  {
    Username = username;
    Userid = userid;
    Key = key;
    SessionToken = sessionToken;
  }

  public static LoginToken Parse(string loginCode, SessionToken sessionToken)
  {
    if (!s_tokenCodeParseRegex.IsMatch(loginCode))
      throw new ArgumentException("Login Code is invalid");

    var regexParseResult = s_tokenCodeParseRegex.Match(loginCode);

    var userName = regexParseResult.Groups["userName"].Value;
    var userId = regexParseResult.Groups["userId"].Value;
    var key = regexParseResult.Groups["key"].Value;

    return new LoginToken(userName, userId, key, sessionToken);
  }
}
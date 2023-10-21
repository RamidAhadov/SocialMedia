using Core.Utilities.Security.Jwt;

namespace DataContracts.Models;

public class TokenModel
{
    public AccessToken Token { get; set; }
}
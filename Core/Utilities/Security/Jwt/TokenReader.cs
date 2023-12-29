using System.IdentityModel.Tokens.Jwt;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Microsoft.IdentityModel.Claims;

namespace Core.Utilities.Security.Jwt;

public static class TokenReader
{
    public static UserDto DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var readToken = tokenHandler.ReadJwtToken(token);

        var userId = readToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var name = readToken.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        var surName = readToken.Claims.First(c => c.Type == ClaimTypes.Surname).Value;
        var userEmail = readToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value;
        var userName = readToken.Claims.First(c=>c.Type == ClaimTypes.GivenName).Value;
        var photoUrl = readToken.Claims.First(c => c.Type == ClaimTypes.Uri).Value;
        var createdDate = readToken.Claims.First(c => c.Type == ClaimTypes.Version).Value;
        var user =  new UserDto
        {
            Id = int.Parse(userId),
            FirstName = name,
            LastName = surName,
            UserName = userName,
            Email = userEmail,
            ProfilePhoto = photoUrl,
            CreatedDate = createdDate
        };
        return user;
    }
}
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Core.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Email,email));
    }
    public static void AddName(this ICollection<Claim> claims, string name)
    {
        claims.Add(new Claim(ClaimTypes.Name,name));
    }
    public static void AddSurName(this ICollection<Claim> claims, string surName)
    {
        claims.Add(new Claim(ClaimTypes.Surname,surName));
    }
    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(ClaimTypes.NameIdentifier,nameIdentifier));
    }
    public static void AddUserName(this ICollection<Claim> claims, string userName)
    {
        claims.Add(new Claim(ClaimTypes.GivenName,userName));
    }
    public static void AddProfilePhoto(this ICollection<Claim> claims, string photoUrl)
    {
        claims.Add(new Claim(ClaimTypes.Uri,photoUrl));
    }
    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role=>claims.Add(new Claim(ClaimTypes.Role,role)));
    }
}
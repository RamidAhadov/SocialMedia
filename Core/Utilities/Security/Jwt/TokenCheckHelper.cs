using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.ObjectiveC;
using Core.Utilities.Results;
using Core.Utilities.Security.Encryption;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt;

public static class TokenCheckHelper
{
    public static IResult Check(string? token)
    {
        if (String.IsNullOrEmpty(token))
        {
            return new ErrorResult("Token is not created");
        }
        var securityKey = SecurityKeyHelper.CreateSecurityKey("ThinkSocialMediaAndBlogPlatform1");
    
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "www.engin.com",
                ValidAudience = "www.engin.com",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey
            }, out var validatedToken);
        }
        catch (Exception ex)
        {
            return new ErrorResult("Token is not valid");
        }

        return new SuccessResult();
    }
}
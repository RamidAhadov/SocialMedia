using DataAccess.Concrete.Dtos;
using Microsoft.AspNetCore.Identity;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete;

public class AspNetAuthService:IAspNetAuthService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AspNetAuthService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> RegisterUser(UserForRegisterDto userForRegisterDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = userForRegisterDto.UserName,
            Email = userForRegisterDto.Email
        };

        var result = await _userManager.CreateAsync(identityUser, userForRegisterDto.Password);
        if (result.Succeeded)
        {
            return true;
        }
        else
        {
            var errorMessages = result.Errors.Select(error => error.Description);
            return false;
        }

    }

    public async Task<bool> LoginUser(UserForLoginDto userForLoginDto)
    {
        if (userForLoginDto.LoginInfo.Contains("@"))
        {
            var identityUser = await _userManager.FindByEmailAsync(userForLoginDto.LoginInfo);

            if (identityUser != null)
            {
                return false;
            }
            
            return await _userManager.CheckPasswordAsync(identityUser, userForLoginDto.Password);
        }
        else
        {
            var identityUser = await _userManager.FindByNameAsync(userForLoginDto.Password);
            
            if (identityUser != null)
            {
                return false;
            }
            
            return await _userManager.CheckPasswordAsync(identityUser, userForLoginDto.Password);
        }
    }
}
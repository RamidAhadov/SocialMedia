using DataAccess.Concrete.Dtos;
using Microsoft.AspNetCore.Identity;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete;

public class AspNetAuthService : IAspNetAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AspNetAuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        var user = await _userManager.FindByNameAsync("farid2000");
        var email = user.Email;
        var result =
            await _signInManager.PasswordSignInAsync(userForLoginDto.LoginInfo, userForLoginDto.Password, false, false);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }
//     if (userForLoginDto.LoginInfo.Contains("@"))
    //     {
    //         var identityUser = await _userManager.FindByEmailAsync(userForLoginDto.LoginInfo);
    //
    //         if (identityUser != null)
    //         {
    //             return false;
    //         }
    //
    //         if (await _userManager.CheckPasswordAsync(identityUser, userForLoginDto.Password))
    //         {
    //             await _signInManager.SignInAsync(identityUser, false, null);
    //             return true;
    //         }
    //
    //         return false;
    //
    //     }
    //     else
    //     {
    //         var identityUser = await _userManager.FindByNameAsync(userForLoginDto.Password);
    //         
    //         if (identityUser != null)
    //         {
    //             return false;
    //         }
    //         
    //         if (await _userManager.CheckPasswordAsync(identityUser, userForLoginDto.Password))
    //         {
    //             await _signInManager.SignInAsync(identityUser, false, null);
    //             return true;
    //         }
    //
    //         return false;
    //     }
    // }
}
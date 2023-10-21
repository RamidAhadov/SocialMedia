using DataAccess.Concrete.Dtos;

namespace WebAPI.Services.Abstract;

public interface IAspNetAuthService
{
    Task<bool> RegisterUser(UserForRegisterDto userForRegisterDto);
    Task<bool> LoginUser(UserForLoginDto userForLoginDto);
}
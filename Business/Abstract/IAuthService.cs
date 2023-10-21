using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete.Dtos;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
    IDataResult<User> Login(UserForLoginDto userForLoginDto);
    IResult UserExists(string email,string userName);
    IResult UserControl(string loginInfo);
    IResult PasswordWrong(string password, string confirmedPassword);
    IDataResult<AccessToken> CreateToken(User user);
    
    //IDataResult<User> DecodeToken(string token);
}
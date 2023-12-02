using System.Runtime.InteropServices.ComTypes;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Dtos;

namespace Business.Concrete;

public class AuthManager:IAuthService
{
    private IUserService _userService;
    private ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash,out passwordSalt);
        var user = new User
        {
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            Email = userForRegisterDto.Email,
            UserName = userForRegisterDto.UserName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = false,
            ProfilePhotoUrl = "https://res.cloudinary.com/do2bvloo2/image/upload/v1697217971/gf0ivu9uusffo98rhsp4.png",
            CreatedDate = DateTime.Now
        };
        _userService.Add(user);
        return new SuccessDataResult<User>(user, Messages.UserCreated);
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByLoginInfo(userForLoginDto.LoginInfo);
        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck);
    }

    public IResult UserExists(string email,string userName)
    {
        var resultEmail = _userService.GetByEmail(email);
        var resultUserName = _userService.GetByUserName(userName);
        if (resultEmail != null)
        {
            return new ErrorResult(Messages.UserExists);
        }
        
        if (resultUserName.Data != null)
        {
            return new ErrorResult(Messages.UserExists);
        }

        return new SuccessResult();
    }

    public IResult UserControl(string loginInfo)
    {
        var result = _userService.GetByLoginInfo(loginInfo);
        if (result == null)
        {
            return new ErrorResult(Messages.NotExists);
        }

        return new SuccessResult();
    }

    public IResult PasswordWrong(string password, string confirmedPassword)
    {
        if (password != confirmedPassword)
        {
            return new ErrorResult(Messages.PasswordMatchError);
        }

        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateToken(User user)
    {
        var claims = _userService.GetClaims(user);
        var token = _tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(token);
    }

    // public IDataResult<User> DecodeToken(string token)
    // {
    //     if (!string.IsNullOrEmpty(token))
    //     {
    //         var user = _tokenHelper.DecodeToken(token);
    //         return new SuccessDataResult<User>(user);
    //     }
    //     return new ErrorDataResult<User>();
    // }
}
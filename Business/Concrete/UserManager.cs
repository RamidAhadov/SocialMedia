using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.EntityMap;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Newtonsoft.Json;

namespace Business.Concrete;

public class UserManager:IUserService
{
    private IUserDao _userDao;

    public UserManager(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public List<User> GetAll()
    {
        return _userDao.GetList();
    }

    public IDataResult<List<UserDto>> GetBySearch(string userName)
    {
        try
        {
            string convertedName = JsonConvert.DeserializeObject<string>(userName);
            var userList = _userDao.GetList(u=>u.UserName.Contains(convertedName));

            //DateTime to string error
            var mappedUserList = EntityMapper<UserDto, User>.Map(userList, "ProfilePhoto", "ProfilePhotoUrl");

            return new SuccessDataResult<List<UserDto>>(mappedUserList);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<List<UserDto>>();
        }
    }

    public List<OperationClaim> GetClaims(User user)
    {
        return _userDao.GetClaims(user);
    }

    public void Add(User user)
    {
        _userDao.Add(user);
    }

    public User GetById(int id)
    {
        return _userDao.Get(u => u.Id == id);
    }

    public User GetByEmail(string email)
    {
        return _userDao.Get(u => u.Email == email);
    }

    public IDataResult<UserDto> GetByUserName(string userName)
    {
        var user = _userDao.Get(u => u.UserName == userName);
        if (user is not null)
        {
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedDate = user.CreatedDate.ToString("yyyy MMMM dd"),
                ProfilePhoto = user.ProfilePhotoUrl
            };
            return new SuccessDataResult<UserDto>(userDto);
        }

        return new ErrorDataResult<UserDto>();
    }

    public IDataResult<UserDto> GetByToken(string token)
    {
        try
        {
            var user = TokenReader.DecodeToken(token);
            return new SuccessDataResult<UserDto>(user);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<UserDto>();
        }
    }

    public User GetByLoginInfo(string loginInfo)
    {
        if (loginInfo.Contains("@"))
        {
            return _userDao.Get(u => u.Email == loginInfo);
        }

        return _userDao.Get(u => u.UserName == loginInfo);
    }

    public UserDto GetUserDtoById(int id)
    {
        var user = _userDao.Get(u => u.Id == id);
        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ProfilePhoto = user.ProfilePhotoUrl,
            UserName = user.UserName,
            CreatedDate = user.CreatedDate.ToString()
        };
        return userDto;
    }
}
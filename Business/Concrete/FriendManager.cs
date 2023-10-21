using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Dtos;

namespace Business.Concrete;

public class FriendManager:IFriendService
{
    private IFriendDao _friendDao;
    private IUserDao _userDao;

    public FriendManager(IFriendDao friendDao, IUserDao userDao)
    {
        _friendDao = friendDao;
        _userDao = userDao;
    }

    public IDataResult<List<UserDto>> ShowFriends(int userId)
    {
        var user = _userDao.Get(u => u.Id == userId);
        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email
        };
        var list = _friendDao.GetFriends(userDto);
        if (list != null)
        {
            return new SuccessDataResult<List<UserDto>>(list);
        }

        return new ErrorDataResult<List<UserDto>>();
    }

    public IDataResult<List<Friend>> CheckFriend(int userId)
    {
        var list = _friendDao.GetList(f => f.UserId == userId);
        if (list != null)
        {
            return new SuccessDataResult<List<Friend>>(list);
        }

        return new ErrorDataResult<List<Friend>>();
    }

    public IResult DeleteFriend(int friendId)
    {
        throw new NotImplementedException();
    }
}
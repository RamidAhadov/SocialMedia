using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.CombineLists;
using Core.Utilities.EntityMap;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Dtos;

namespace Business.Concrete;

public class FriendManager:IFriendService
{
    private IFriendDao _friendDao;
    private IFriendRequestDao _friendRequestDao;
    private IUserDao _userDao;

    public FriendManager(IFriendDao friendDao, IUserDao userDao, IFriendRequestDao friendRequestDao)
    {
        _friendDao = friendDao;
        _userDao = userDao;
        _friendRequestDao = friendRequestDao;
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

    public IResult CheckFriend(int userId,int friendId)
    {
        // try
        // {
        //     var friendList = _friendDao.GetList(f => f.UserId == userId);
        //     var friendRequestListForSender = _friendRequestDao.GetList(fr=>fr.SenderId == userId 
        //                                                                    && fr.Status == "Pending");
        //     var friendRequestListForReceiver = _friendRequestDao.GetList(fr=>fr.ReceiverId == userId 
        //                                                                      && fr.Status == "Pending");
        //     var mappedFriendRequestListForSender = EntityMapper<Friend,FriendRequest>.Map(friendRequestListForSender,
        //         "UserId","SenderId","FriendId","ReceiverId");
        //     var mappedFriendRequestListForReceiver = EntityMapper<Friend,FriendRequest>.Map(friendRequestListForReceiver,
        //         "UserId","SenderId","FriendId","ReceiverId");
        //
        //     var list = CombineLists.Combine(friendList, mappedFriendRequestListForSender,
        //         mappedFriendRequestListForReceiver);
        //
        //     return new SuccessDataResult<List<Friend>>(list);
        // }
        // catch (Exception e)
        // {
        //     return new ErrorDataResult<List<Friend>>();
        // }
        
        //If they are friend
        var friendship = _friendDao.Get(f => f.UserId == userId && f.FriendId == friendId);

        var friendRequest = _friendRequestDao.Get(fr => fr.ReceiverId == userId
                                                        && fr.SenderId == friendId
                                                        && fr.Status == "Pending");

        if (friendship == null && friendRequest == null)
        {
            return new SuccessResult();
        }

        return new ErrorResult();

    }

    public IResult DeleteFriend(int friendId)
    {
        throw new NotImplementedException();
    }
}
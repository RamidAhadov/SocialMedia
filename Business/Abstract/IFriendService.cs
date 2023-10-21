using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.Results;
using DataAccess.Concrete.Dtos;

namespace Business.Abstract;

public interface IFriendService
{
    IDataResult<List<UserDto>> ShowFriends(int requesterId);
    IDataResult<List<Friend>> CheckFriend(int userId);
    IResult DeleteFriend(int firendId);
}
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IFriendRequestService
{
    IDataResult<List<FriendRequest>> ShowUserRequests(int id);
    IResult SendRequest(string token, int receiverId);
    IResult AcceptFriend(int senderId, int receiverId);
    IResult DeclineFriend(int senderId, int receiverId);
    IDataResult<string> RequestStatus(int senderId, int receiverId);
}
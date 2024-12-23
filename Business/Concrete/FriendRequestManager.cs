using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;

namespace Business.Concrete;

public class FriendRequestManager:IFriendRequestService
{
    private IFriendRequestDao _requestDao;
    private IFriendDao _friendDao;

    public FriendRequestManager(IFriendRequestDao requestDao, IFriendDao friendDao)
    {
        _requestDao = requestDao;
        _friendDao = friendDao;
    }

    public IDataResult<List<FriendRequest>> ShowUserRequests(int id)
    {
        var list = _requestDao.GetList(r => r.ReceiverId == id && r.Status == "Pending");
        return new SuccessDataResult<List<FriendRequest>>(list);
    }

    public IResult SendRequest(string token, int receiverId)
    {
        var user = TokenReader.DecodeToken(token);
        var result = _requestDao.Get(r => r.SenderId == user.Id 
                                          && r.ReceiverId == receiverId);
        var request = new FriendRequest
        {
            SenderId = user.Id,
            SenderName = user.FirstName + " " + user.LastName,
            ReceiverId = receiverId,
            Date = DateTime.Now,
            Status = "Pending"
        };
        if (result==null)
        {
            _requestDao.Add(request);
            return new SuccessResult();
        }
        _requestDao.Delete(result);
        return new ErrorResult();
    }

    public IResult AcceptFriend(int senderId, int receiverId)
    {
        var connectionFirst = new Friend
        {
            UserId = receiverId,
            FriendId = senderId
        };
        var connectionSecond = new Friend
        {
            UserId = senderId,
            FriendId = receiverId
        };
        _friendDao.Add(connectionFirst);
        _friendDao.Add(connectionSecond);
        var request = _requestDao.Get(r=>r.SenderId == senderId 
                                         && r.ReceiverId == receiverId
                                         && r.Status == "Pending");
        request.Status = "Accepted";
        _requestDao.Update(request);
        return new SuccessResult();
    }

    public IResult DeclineFriend(int senderId, int receiverId)
    {
        var request = _requestDao.Get(r=>r.SenderId == senderId 
                                         && r.ReceiverId == receiverId
                                         && r.Status == "Pending");
        request.Status = "Declined";
        _requestDao.Update(request);
        return new SuccessResult();
    }

    public IDataResult<string> RequestStatus(int senderId, int receiverId)
    {
        var request = _requestDao.Get(r => r.SenderId == senderId && r.ReceiverId == receiverId);
        var oppositeRequest = _requestDao.Get(r => r.SenderId == receiverId && r.ReceiverId == senderId);
        
        //0 - Send Request
        //1 - Cancel Request
        //2 - Delete Friend

        if (request == null && oppositeRequest == null)
        {
            return new SuccessDataResult<string>(data: "0");
        }

        if (request.Status == "Pending")
        {
            return new SuccessDataResult<string>(data: "1");
        }
        
        if (request.Status == "Accepted")
        {
            return new SuccessDataResult<string>(data: "2");
        }
        
        if (request.Status == "Declined")
        {
            return new SuccessDataResult<string>(data: "0");
        }

        return new ErrorDataResult<string>();
    }
}
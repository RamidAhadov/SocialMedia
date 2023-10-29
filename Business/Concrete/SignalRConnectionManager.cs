using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;

namespace Business.Concrete;

public class SignalRConnectionManager:ISignalRConnectionService
{
    private IUserConnectionIdDao _connectionIdDao;

    public SignalRConnectionManager(IUserConnectionIdDao connectionIdDao)
    {
        _connectionIdDao = connectionIdDao;
    }

    public IResult RecordConnectionId(string token,string connectionId)
    {
        var userDto = TokenReader.DecodeToken(token);
        var checkConnectionId = _connectionIdDao.Get(c => c.UserId == userDto.Id);
        if (connectionId != null && token != null)
        {
            if (checkConnectionId is not null)
            {
                var userConnectionId = new UserConnectionId
                {
                    Id = checkConnectionId.Id,
                    UserId = checkConnectionId.UserId,
                    UserName = checkConnectionId.UserName,
                    ConnectionId = connectionId,
                    Status = "Online"
                };
                _connectionIdDao.Update(userConnectionId);
                return new SuccessResult(Messages.ConenctionIdRecorded);
            }

            if (checkConnectionId is null)
            {
                var userConnectionId = new UserConnectionId
                {
                    UserId = userDto.Id,
                    UserName = userDto.UserName,
                    ConnectionId = connectionId,
                    Status = "Online"
                };
                _connectionIdDao.Add(userConnectionId);
                return new SuccessResult(Messages.ConenctionIdRecorded);
            }
        }

        return new ErrorResult();
    }

    public IDataResult<string> GetConnectionId(string friendUserName)
    {
        var connectionId = _connectionIdDao.Get(c => c.UserName == friendUserName &&
                                                     c.Status == "Online");
        if (connectionId != null)
        {
            return new SuccessDataResult<string>(data:connectionId.ConnectionId);
        }

        return new ErrorDataResult<string>(Messages.UserIsNotOnline);
    }

    public IDataResult<string> GetLastSeen(string userName)
    {
        var userConnectionId = _connectionIdDao.Get(uc => uc.UserName == userName &&
                                                          uc.Status == "Offline");
        TimeSpan? difference = DateTime.Now - userConnectionId.LastSeenDate;
        int days = difference.Value.Days;
        int hours = difference.Value.Hours;
        int minutes = difference.Value.Minutes;
        int seconds = difference.Value.Seconds;
        var lastSeen = userConnectionId.LastSeenDate.Value.ToString("yyyy MMMM dd");
        if (days > 1)
        {
            return new SuccessDataResult<string>(data: $"Last seen: {lastSeen}");
        }

        if (days == 1)
        {
            return new SuccessDataResult<string>(data: $"Last seen yesterday");
        }

        if (hours is > 1 and < 23)
        {
            return new SuccessDataResult<string>(data: $"Last seen {hours} hours ago");
        }

        if (hours == 1)
        {
            return new SuccessDataResult<string>(data: $"Last seen an hour ago");
        }

        if (minutes is > 1 and < 59)
        {
            return new SuccessDataResult<string>(data: $"Last seen {minutes} minutes ago");
        }

        if (minutes < 1)
        {
            return new SuccessDataResult<string>(data: "Last seen just now");
        }

        return new ErrorDataResult<string>();
    }


    public IResult UpdateStatus(string token)
    {
        try
        {
            var userDto = TokenReader.DecodeToken(token);
            int userId = userDto.Id;
            var userConnectionId = _connectionIdDao.Get(
                uc => uc.UserId == userId &&
                      uc.Status == "Online");
            userConnectionId.Status = "Offline";
            userConnectionId.LastSeenDate = DateTime.Now;
            _connectionIdDao.Update(userConnectionId);
            return new SuccessResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e.InnerException.Message);
        }
    }

    public IResult DeleteConnectionId(string token)
    {
        var userDto = TokenReader.DecodeToken(token);
        int userId = userDto.Id;
        var userConnectionId = _connectionIdDao.Get(
            uc => uc.UserId == userId);
        if (userConnectionId != null)
        {
            _connectionIdDao.Delete(userConnectionId);
            return new SuccessResult();
        }

        return new ErrorResult();
    }

    public IDataResult<string> CheckStatus(string userName)
    {
        var status = _connectionIdDao.GetLastConnectionId(userName);
        if (status=="Online")
        {
            return new SuccessDataResult<string>(data:status);
        }

        return new ErrorDataResult<string>(data: status);
    }
}
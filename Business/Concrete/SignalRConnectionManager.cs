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
            if (checkConnectionId != null)
            {
                _connectionIdDao.Delete(checkConnectionId);
            }
            var userConnectionId = new UserConnectionId
            {
                UserId = userDto.Id,
                UserName = userDto.UserName,
                ConnectionId = connectionId
            };
            _connectionIdDao.Add(userConnectionId);
            return new SuccessResult(Messages.ConenctionIdRecorded);
        }

        return new ErrorResult();
    }

    public IDataResult<string> GetConnectionId(string friendUserName)
    {
        var connectionId = _connectionIdDao.Get(c => c.UserName == friendUserName);
        if (connectionId != null)
        {
            return new SuccessDataResult<string>(data:connectionId.ConnectionId);
        }

        return new ErrorDataResult<string>(Messages.UserIsNotOnline);
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
}
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface ISignalRConnectionService
{
    IResult RecordConnectionId(string token, string connectionId);
    IDataResult<string> GetConnectionId(string friendUserName);
    IDataResult<string> GetConnectionIdById(int id);
    IDataResult<string> GetLastSeen(string userName);
    IResult UpdateStatus(string token);
    IResult DeleteConnectionId(string token);
    IDataResult<string> CheckStatus(string userName);
    IDataResult<List<UserConnectionId>> GetOnlineUserConnectionIds(List<int> userIds);
}
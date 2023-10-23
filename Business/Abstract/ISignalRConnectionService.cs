using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface ISignalRConnectionService
{
    IResult RecordConnectionId(string token, string connectionId);
    IDataResult<string> GetConnectionId(string friendUserName);
    IResult DeleteConnectionId(string token);
}
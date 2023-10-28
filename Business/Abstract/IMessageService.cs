using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Abstract;

public interface IMessageService
{
    IResult RecordMessage(MessageDto messageDto);
    IDataResult<List<Message>> GetMessages(int senderId, int receiverId);
}
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Abstract;

public interface IMessageService
{
    IDataResult<Message> RecordMessage(MessageDto messageDto);
    IDataResult<List<Message>> GetMessages(int senderId, int receiverId);
    IDataResult<Message> UpdateMessageStatus(int messageId);
    IDataResult<List<Message>> GetNotReceivedMessages(string token);
    IResult UpdateMessagesStatusOnLogin(List<Message> messages);

}
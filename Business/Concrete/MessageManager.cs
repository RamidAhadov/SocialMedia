using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.CombineData;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Concrete;

public class MessageManager:IMessageService
{
    private IMessageDao _messageDao;

    public MessageManager(IMessageDao messageDao)
    {
        _messageDao = messageDao;
    }

    public IDataResult<Message> RecordMessage(MessageDto messageDto)
    {
        if (messageDto != null || messageDto.MessageText != "")
        {
            var conversationName = CombineData.CombineInteger(messageDto.SenderId, messageDto.ReceiverId);
            var message = new Message
            {
                SenderId = messageDto.SenderId,
                ReceiverId = messageDto.ReceiverId,
                ConversationName = conversationName,
                Date = DateTime.Now,
                Status = 1,
                MessageText = messageDto.MessageText
            };
            _messageDao.Add(message);
            return new SuccessDataResult<Message>(message);
        }

        return new ErrorDataResult<Message>(message:Messages.MessageNotSent);
    }

    public IDataResult<List<Message>> GetMessages(int senderId, int receiverId)
    {
        // var list = _messageDao.GetList(m => m.SenderId == senderId && 
        //                                     m.ReceiverId == receiverId ||
        //                                     m.SenderId == receiverId &&
        //                                     m.ReceiverId == senderId);
        
        string conversationName = CombineData.CombineInteger(senderId, receiverId);
        var list = _messageDao.GetList(m=>m.ConversationName == conversationName);
        if (list != null)
        {
            return new SuccessDataResult<List<Message>>(list);
        }

        return new ErrorDataResult<List<Message>>();
    }

    public IDataResult<Message> UpdateMessageStatus(int messageId)
    {
        var message = _messageDao.Get(m => m.Id == messageId);
        if (message != null)
        {
            message.Status = 2;
            _messageDao.Update(message);
            return new SuccessDataResult<Message>(message);
        }

        return new ErrorDataResult<Message>();
    }

    public IDataResult<List<Message>> GetUnreadMessages(string token)
    {
        var userDto = TokenReader.DecodeToken(token);
        var list = _messageDao.GetList(m => m.ReceiverId == userDto.Id && m.Status == 1);
        if (list.Count != 0)
        {
            return new SuccessDataResult<List<Message>>(list);
        }

        return new ErrorDataResult<List<Message>>();
    }

    public IResult UpdateMessagesStatusOnLogin(List<Message> messages)
    {
        try
        {
            foreach (var message in messages)
            {
                message.Status = 2;
                _messageDao.Update(message);
            }

            return new SuccessResult();
        }
        catch (Exception e)
        {
            return new ErrorResult();
        }
    }

    public IDataResult<List<UnreadMessageDto>> MapMessages(List<Message> messages, List<UserConnectionId> userConnectionIds)
    {
        var list = new List<UnreadMessageDto>();
        foreach (var userConnectionId in userConnectionIds)
        {
            var messageList = new List<int>();
            foreach (var message in messages)
            {
                if (message.SenderId == userConnectionId.UserId)
                {
                    messageList.Add(message.Id);
                }
            }
            var unreadMessageDto = new UnreadMessageDto
            {
                ConnectionId = userConnectionId.ConnectionId,
                MessageIds = messageList
            };
            list.Add(unreadMessageDto);
        }

        if (list.Count == 0 || list is null)
        {
            return new ErrorDataResult<List<UnreadMessageDto>>();
        }

        return new SuccessDataResult<List<UnreadMessageDto>>(list);
    }
}
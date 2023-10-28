using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

    public IResult RecordMessage(MessageDto messageDto)
    {
        if (messageDto != null || messageDto.MessageText != "")
        {
            var message = new Message
            {
                SenderId = messageDto.SenderId,
                ReceiverId = messageDto.ReceiverId,
                Date = DateTime.Now,
                MessageText = messageDto.MessageText
            };
            _messageDao.Add(message);
            return new SuccessResult();
        }

        return new ErrorResult(message:Messages.MessageNotSent);
    }

    public IDataResult<List<Message>> GetMessages(int senderId, int receiverId)
    {
        var list = _messageDao.GetList(m => m.SenderId == senderId && 
                                            m.ReceiverId == receiverId ||
                                            m.SenderId == receiverId &&
                                            m.ReceiverId == senderId);
        if (list != null)
        {
            return new SuccessDataResult<List<Message>>(list);
        }

        return new ErrorDataResult<List<Message>>();
    }
}
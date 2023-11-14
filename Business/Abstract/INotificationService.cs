using Business.Models;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Abstract;

public interface INotificationService
{
    IDataResult<NotificationModel> RecordNotification(NotificationDto dto);
    IDataResult<List<RequestModel>> GetFriendRequestNotifications(string token);
    IDataResult<List<NotificationModel>> GetNotifications(string token);
}
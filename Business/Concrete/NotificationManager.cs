using Business.Abstract;
using Business.Constants;
using Business.Models;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Core.Utilities.TimeSpan;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Concrete;

public class NotificationManager:INotificationService
{
    private readonly IUserDao _userDao;
    private readonly IFriendRequestDao _friendRequestDao;
    private readonly INotificationDao _notificationDao;

    public NotificationManager(IUserDao userDao, IFriendRequestDao friendRequestDao, INotificationDao notificationDao)
    {
        _userDao = userDao;
        _friendRequestDao = friendRequestDao;
        _notificationDao = notificationDao;
    }

    public IDataResult<NotificationModel> RecordNotification(NotificationDto dto)
    {
        try
        {
            var sender = TokenReader.DecodeToken(dto.token);
            var notification = new Notification
            {
                ReceiverId = dto.ReceiverId,
                SenderId = sender.Id,
                Template = dto.Template,
                NotificationDate = DateTime.Now
            };
            if (notification.SenderId != notification.ReceiverId)
            {
                _notificationDao.Add(notification);
                
                var model = new NotificationModel
                {
                    SenderId = sender.Id,
                    ReceiverId = dto.ReceiverId,
                    ProfilePhoto = sender.ProfilePhoto,
                    NotificationDate = "Just now",
                    NotificationId = sender.Id + dto.ReceiverId
                };
                if (dto.Template == "ACF")
                {
                    model.NotificationContent =
                        Notifications.AcceptFriendRequestTemplate(sender.FirstName, sender.LastName);
                }
                if (dto.Template == "LKP")
                {
                    model.NotificationContent =
                        Notifications.LikePostTemplate(sender.FirstName, sender.LastName);
                }
                if (dto.Template == "LKC")
                {
                    model.NotificationContent =
                        Notifications.LikeCommentTemplate(sender.FirstName, sender.LastName);
                }
                if (dto.Template == "WRC")
                {
                    model.NotificationContent =
                        Notifications.WriteCommentTemplate(sender.FirstName, sender.LastName);
                }
                return new SuccessDataResult<NotificationModel>(model);
            }

            return new ErrorDataResult<NotificationModel>();
        }
        catch (Exception e)
        {
            return new ErrorDataResult<NotificationModel>();
        }
    }

    public IDataResult<List<RequestModel>> GetFriendRequestNotifications(string token)
    {
        try
        {
            var user = TokenReader.DecodeToken(token);
            var friendRequests = _friendRequestDao.GetList(fr => fr.ReceiverId == user.Id &&
                                                                 fr.Status == "Pending");
            var list = new List<RequestModel>();
            foreach (var friendRequest in friendRequests)
            {
                var requester = _userDao.Get(u => u.Id == friendRequest.SenderId);
                var model = new RequestModel
                {
                    UserId = user.Id,
                    SenderId = friendRequest.SenderId,
                    RequestId = friendRequest.Id,
                    RequestContent = Notifications.FriendRequestTemplate(requester.FirstName,requester.LastName),
                    ProfilePhoto = requester.ProfilePhotoUrl,
                    RequestDate = TimeSpanCalculator.CalculateDifference(friendRequest.Date)
                };
                list.Add(model);
            }

            if (list.Count > 0)
            {
                return new SuccessDataResult<List<RequestModel>>(list);
            }

            return new ErrorDataResult<List<RequestModel>>();
        }
        catch (Exception e)
        {
            return new ErrorDataResult<List<RequestModel>>();
        }
    }

    public IDataResult<List<NotificationModel>> GetNotifications(string token)
    {
        var user = TokenReader.DecodeToken(token);
        var notifications = _notificationDao.GetList(n => n.ReceiverId == user.Id);
        var list = new List<NotificationModel>();
        foreach (var notification in notifications)
        {
            var sender = _userDao.Get(u => u.Id == notification.SenderId);
            var model = new NotificationModel();
            
            model.NotificationId = notification.Id;
            model.SenderId = notification.SenderId;
            model.ReceiverId = notification.ReceiverId;
            model.ProfilePhoto = sender.ProfilePhotoUrl;
            model.NotificationDate = TimeSpanCalculator.CalculateDifference(notification.NotificationDate);
            if (notification.Template == "ACF")
            {
                model.NotificationContent =
                    Notifications.AcceptFriendRequestTemplate(sender.FirstName, sender.LastName);
            }
            if (notification.Template == "LKP")
            {
                model.NotificationContent =
                    Notifications.LikePostTemplate(sender.FirstName, sender.LastName);
            }
            if (notification.Template == "LKC")
            {
                model.NotificationContent =
                    Notifications.LikeCommentTemplate(sender.FirstName, sender.LastName);
            }
            if (notification.Template == "WRC")
            {
                model.NotificationContent =
                    Notifications.WriteCommentTemplate(sender.FirstName, sender.LastName);
            }
            list.Add(model);
        }

        if (list.Count > 0)
        {
            return new SuccessDataResult<List<NotificationModel>>(list);
        }

        return new ErrorDataResult<List<NotificationModel>>();
    }

    public IDataResult<string> NotificationsCount(string token)
    {
        var user = TokenReader.DecodeToken(token);
        try
        {
            int count;
            var notifications = _notificationDao.GetList(n => 
                n.ReceiverId == user.Id);
            var friendRequests = _friendRequestDao.GetList(fr => 
                fr.ReceiverId == user.Id &&
                fr.Status == "Pending");
            count = notifications.Count + friendRequests.Count;
            if (count == 0)
            {
                return new ErrorDataResult<string>(message:"There is not notification.");
            }
            if (count > 20)
            {
                return new SuccessDataResult<string>(data: "20+");
            }
            return new SuccessDataResult<string>(data: count.ToString());
        }
        catch (Exception e)
        {
            return new ErrorDataResult<string>(e.Message);
        }
    }

    public IResult DeleteNotification(string token)
    {
        var user = TokenReader.DecodeToken(token);
        var notifications = _notificationDao.GetList(n => n.ReceiverId == user.Id);
        try
        {
            foreach (var notification in notifications)
            {
                _notificationDao.Delete(notification);
            }

            return new SuccessResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e.Message);
        }
    }
}
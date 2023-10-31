namespace Business.Models;

public class NotificationModel
{
    public int ReceiverId { get; set; }
    public int SenderId { get; set; }
    public int NotificationId { get; set; }
    public string NotificationContent { get; set; }
    public string ProfilePhoto { get; set; }
    public string NotificationDate { get; set; }
}
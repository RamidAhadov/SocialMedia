namespace Entities.Concrete;

public class Notification:IEntity
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Template { get; set; }
    public DateTime NotificationDate { get; set; }
}
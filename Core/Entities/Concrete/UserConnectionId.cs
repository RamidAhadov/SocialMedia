namespace Core.Entities.Concrete;

public class UserConnectionId:IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string ConnectionId { get; set; }
    public string Status { get; set; }
    public DateTime? LastSeenDate { get; set; }
}
namespace Core.Entities.Concrete;

public class Friend:IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FriendId { get; set; }
}
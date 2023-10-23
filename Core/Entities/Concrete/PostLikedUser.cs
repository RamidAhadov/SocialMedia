namespace Core.Entities.Concrete;

public class PostLikedUser:IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}
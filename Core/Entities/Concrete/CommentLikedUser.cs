namespace Core.Entities.Concrete;

public class CommentLikedUser:IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CommentId { get; set; }
}
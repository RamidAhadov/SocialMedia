namespace Core.Entities.Concrete;

public class CommentOfPost:IEntity
{
    public int Id { get; set; }
    public int CommentId { get; set; }
    public int PostId { get; set; }
}
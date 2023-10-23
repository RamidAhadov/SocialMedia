namespace Core.Entities.Concrete;

public class Post:IEntity
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public string AuthorUserName { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public DateTime PostDate { get; set; }
    public string PostText { get; set; }

    public List<Comment> Comments { get; set; }
}
namespace Core.Entities.Concrete;

public class Comment:IEntity
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int AuthorId { get; set; }
    public string AuthorUserName { get; set; }
    public int LikeCount { get; set; }
    public DateTime Date { get; set; }
    public string CommentText { get; set; }
}
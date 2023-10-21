namespace DataContracts.Models;

public class CommentForAddDto
{
    public int PostId { get; set; }
    public string CommentText { get; set; }
    public string Token { get; set; }
}
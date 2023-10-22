namespace DataContracts.Models;

public class CommentForAddModel
{
    public int PostId { get; set; }
    public string CommentText { get; set; }
    public string Token { get; set; }
}
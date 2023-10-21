using Core.Entities.Concrete;

namespace DataContracts.Models;

public class CommentLikedUserTokenModel
{
    public CommentLikedUser CommentLikedUser { get; set; }
    public string Token { get; set; }
}
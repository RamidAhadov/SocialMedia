using Core.Entities.Concrete;

namespace DataContracts.Models;

public class CommentLikedUsersModel
{
    public List<User> Users { get; set; }
    public int CommentId { get; set; }
}
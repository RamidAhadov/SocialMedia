using Core.Entities.Concrete;

namespace DataContracts.Models;

public class PostLikedUsersModel
{
    public List<User> Users { get; set; }
    public int PostId { get; set; }
}
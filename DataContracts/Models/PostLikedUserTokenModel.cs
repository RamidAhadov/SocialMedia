using Core.Entities.Concrete;

namespace DataContracts.Models;

public class PostLikedUserTokenModel
{
    public PostLikedUser PostLikedUser { get; set; }
    public string Token { get; set; }
}
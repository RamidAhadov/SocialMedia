using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using DataAccess.Concrete.Dtos;
using DataContracts.Models;

namespace MvcWebUI.Models;

public class HomePageViewModel
{
    public List<Post> Posts { get; set; }

    public List<Comment> Comments { get; set; }
    public UserDto UserDto { get; set; }
    public List<PostLikedUsersModel> PostLikedUsersModels { get; set; }
    public List<CommentLikedUsersModel> CommentLikedUsersModels { get; set; }

    public List<FriendRequest> ReceivedRequests { get; set; }
    public List<UserDto> Friends { get; set; }
    //Just for test
    public string PhotoUrl { get; set; }
}
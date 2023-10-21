using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface ILikeService
{
    IResult LikePost(PostLikedUser postLikedUser,string token);
    IResult LikeComment(CommentLikedUser commentLikedUser,string token);
    IResult IsLiked(int commentId,string token);
}
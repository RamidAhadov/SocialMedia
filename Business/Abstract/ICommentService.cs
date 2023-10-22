using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataContracts.Models;

namespace Business.Abstract;

public interface ICommentService
{
    IDataResult<List<Comment>> GetComments();
    IDataResult<List<CommentLikedUsersModel>> GetLikedUsers(List<Comment> comments);
    IDataResult<List<Comment>> GetCommentsByPostId(int postId);
    IDataResult<Comment> AddComment(CommentForAddModel commentForAddModel,string token);
    IResult DeleteComment(Comment comment);
}
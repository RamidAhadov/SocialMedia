using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IPostDao:IEntityRepository<Post>
{
    List<Comment> GetComments(Post post);
    List<User> GetUsers(Post post);
    List<User> GetCommentLikedUsers(Comment comment);
}
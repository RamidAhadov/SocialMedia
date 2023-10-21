using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface ILikePostDao:IEntityRepository<PostLikedUser>
{
    // void LikePost(int postId, int userId);
    // void UnLikePost(int postId, int userId);
}
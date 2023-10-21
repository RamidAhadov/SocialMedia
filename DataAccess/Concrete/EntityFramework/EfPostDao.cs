using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework;

public class EfPostDao:EfEntityRepositoryBase<Post,ThinkContext>,IPostDao
{
    public List<Comment> GetComments(Post post)
    {
        using (var context = new ThinkContext())
        {
            var result = from comment in context.Comments
                join commentOfPost in context.CommentOfPost
                    on comment.Id equals commentOfPost.CommentId
                where commentOfPost.PostId == post.Id
                select new Comment
                {
                    Id = comment.Id,
                    PostId = comment.PostId,
                    AuthorId = comment.AuthorId,
                    AuthorUserName = comment.AuthorUserName,
                    LikeCount = comment.LikeCount,
                    Date = comment.Date,
                    CommentText = comment.CommentText
                };
            return result.ToList();
        }
    }
    public List<User> GetUsers(Post post)
    {
        using (var context = new ThinkContext())
        {
            var result = from user in context.Users
                join likedUser in context.PostLikedUsers
                    on user.Id equals likedUser.UserId
                where likedUser.PostId == post.Id
                select new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName
                };
            return result.ToList();
        }
    }

    public List<User> GetCommentLikedUsers(Comment comment)
    {
        using (var context = new ThinkContext())
        {
            var result = from user in context.Users
                join likedUser in context.CommentLikedUsers
                    on user.Id equals likedUser.UserId
                where likedUser.CommentId == comment.Id
                select new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName
                };
            return result.ToList();
        }
    }
}
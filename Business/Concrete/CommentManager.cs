using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataContracts.Models;

namespace Business.Concrete;

public class CommentManager:ICommentService
{
    private ICommentDao _commentDao;
    private IPostDao _postDao;
    private ICommentOfPostDao _commentOfPostDao;

    public CommentManager(ICommentDao commentDao, IPostDao postDao, ICommentOfPostDao commentOfPostDao)
    {
        _commentDao = commentDao;
        _postDao = postDao;
        _commentOfPostDao = commentOfPostDao;
    }

    public IDataResult<List<Comment>> GetComments()
    {
        return new SuccessDataResult<List<Comment>>(_commentDao.GetList().ToList());
    }

    public IDataResult<List<CommentLikedUsersModel>> GetLikedUsers(List<Comment> comments)
    {
        var commentUsersList = new List<CommentLikedUsersModel>();

        foreach (var comment in comments)
        {
            var usersForComment = _postDao.GetCommentLikedUsers(comment);

            var postUserData = new CommentLikedUsersModel
            {
                CommentId = comment.Id,
                Users = usersForComment.ToList()
            };

            commentUsersList.Add(postUserData);
        }

        if (commentUsersList!=null)
        {
            return new SuccessDataResult<List<CommentLikedUsersModel>>(commentUsersList);
        }

        return new ErrorDataResult<List<CommentLikedUsersModel>>();
    }

    public IDataResult<List<Comment>> GetCommentsByPostId(int postId)
    {
        return new SuccessDataResult<List<Comment>>(_commentDao.GetList(c=>c.PostId == postId));
    }

    public IDataResult<Comment> AddComment(CommentForAddDto commentForAddDto,string token)
    {
        var user = TokenReader.DecodeToken(token);
            var comment = new Comment
            {
                AuthorId = user.Id,
                AuthorUserName = user.UserName,
                CommentText = commentForAddDto.CommentText,
                PostId = commentForAddDto.PostId,
                Date = DateTime.Now,
                LikeCount = 0
            };
            _commentDao.Add(comment);
            var addedComment = _commentDao.Get(c => c.AuthorId == comment.AuthorId 
                                 && c.CommentText == comment.CommentText && c.Date == comment.Date 
                                 && c.PostId == comment.PostId);
            var post = _postDao.Get(p => p.Id == commentForAddDto.PostId);
            post.CommentCount++;
            _postDao.Update(post);
            var commentOfPost = new CommentOfPost
            {
                CommentId = addedComment.Id,
                PostId = post.Id
            };
            _commentOfPostDao.Add(commentOfPost);
            
            return new SuccessDataResult<Comment>(comment);
    }

    public IResult DeleteComment(Comment comment)
    {
        if (comment!=null)
        {
            _commentDao.Delete(comment);
            return new SuccessResult();
        }
        return new ErrorResult();
    }
}
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using TokenReader = Core.Utilities.Security.Jwt.TokenReader;

namespace Business.Concrete;

public class LikeManager:ILikeService
{
    private ILikePostDao _likePostDao;
    private IPostDao _postDao;
    private ILikeCommentDao _likeCommentDao;
    private ICommentDao _commentDao;
    

    public LikeManager(ILikePostDao likePostDao, ILikeCommentDao likeCommentDao, ICommentDao commentDao, IPostDao postDao)
    {
        _likePostDao = likePostDao;
        _likeCommentDao = likeCommentDao;
        _commentDao = commentDao;
        _postDao = postDao;
    }

    public IResult LikePost(PostLikedUser postLikedUser,string token)
    {
        var user = TokenReader.DecodeToken(token);
        var result = _likePostDao.Get(l =>
            l.UserId == user.Id && l.PostId == postLikedUser.PostId);
        var post = _postDao.Get(p => p.Id == postLikedUser.PostId);
        
        if (result == null)
        {
            var likedUser = new PostLikedUser
            {
                PostId = postLikedUser.PostId,
                UserId = user.Id
            };
            _likePostDao.Add(likedUser);
            post.LikeCount ++;
            _postDao.Update(post);
            return new SuccessResult();
        }
        _likePostDao.Delete(result);
        post.LikeCount --;
        return new SuccessResult();
    }

    public IResult LikeComment(CommentLikedUser commentLikedUser, string token)
    {
        var user = TokenReader.DecodeToken(token);
        var result = _likeCommentDao.Get(l =>
            l.UserId == user.Id && l.CommentId == commentLikedUser.CommentId);
        var comment = _commentDao.Get(c => c.Id == commentLikedUser.CommentId);
        
        if (result == null)
        {
            var likedUser = new CommentLikedUser
            {
                CommentId = commentLikedUser.CommentId,
                UserId = user.Id
            };
            _likeCommentDao.Add(likedUser);
            comment.LikeCount++;
            _commentDao.Update(comment);
            return new SuccessResult();
        }

        _likeCommentDao.Delete(result);
        comment.LikeCount--;
        _commentDao.Update(comment);
        return new SuccessResult();
    }

    public IResult IsLiked(int commentId, string token)
    {
        var user = TokenReader.DecodeToken(token);
        var result = _likeCommentDao.Get(
            lc => lc.CommentId == commentId && lc.UserId == user.Id);
        if (result != null)
        {
            return new SuccessResult();
        }
        return new ErrorResult();
    }
}
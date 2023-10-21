using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Dtos;
using DataContracts.Models;

namespace Business.Concrete;

public class PostManager:IPostService
{
    private IPostDao _postDao;

    public PostManager(IPostDao postDao)
    {
        _postDao = postDao;
    }

    public IDataResult<List<Post>> GetPosts()
    {
        var posts = _postDao.GetList();
        foreach (var post in posts)
        {
            post.Comments = _postDao.GetComments(post);
        }
        return new SuccessDataResult<List<Post>>(posts);
    }

    public IDataResult<Post> GetPostByAuthorId(int authorId)
    {
        return new SuccessDataResult<Post>(_postDao.Get(p=>p.AuthorId==authorId));
    }
    public void AddPost(PostForAddDto postForAddDto,string token)
    {
        var user = TokenReader.DecodeToken(token);
        var post = new Post
        {
            AuthorId = user.Id,
            AuthorName = user.FirstName + " " + user.LastName,
            AuthorUserName = user.UserName,
            PostDate = DateTime.Now,
            PostText = postForAddDto.PostText
        };
        _postDao.Add(post);
    }

    public IDataResult<Post> UpdatePost(Post post)
    {
        _postDao.Update(post);
        return new SuccessDataResult<Post>();
    }

    public IDataResult<Post> DeletePost(Post post)
    {
        _postDao.Delete(post);
        return new SuccessDataResult<Post>();
    }

    public IDataResult<List<PostLikedUsersModel>> GetLikedUsers(List<Post> posts)
    {
        var postUsersList = new List<PostLikedUsersModel>();

        foreach (var post in posts)
        {
            var usersForPost = _postDao.GetUsers(post);

            var postUserData = new PostLikedUsersModel
            {
                PostId = post.Id,
                Users = usersForPost.ToList()
            };

            postUsersList.Add(postUserData);
        }

        if (postUsersList!=null)
        {
            return new SuccessDataResult<List<PostLikedUsersModel>>(postUsersList);
        }

        return new ErrorDataResult<List<PostLikedUsersModel>>();
    }
}
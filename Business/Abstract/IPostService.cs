using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Dtos;
using DataContracts.Models;

namespace Business.Abstract;

public interface IPostService
{
    IDataResult<List<Post>> GetPosts();
    IDataResult<Post> GetPostByAuthorId(int authorId);
    IDataResult<List<Post>> GetPostsByAuthorId(int authorId);
    IDataResult<Post> AddPost(PostForAddDto postForAddDto, string token);
    IDataResult<Post> UpdatePost(Post post);
    IDataResult<Post> DeletePost(int postId);
    IDataResult<List<PostLikedUsersModel>> GetLikedUsers(List<Post> posts);
}
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Dtos;
using DataContracts.Models;

namespace Business.Abstract;

public interface IPostService
{
    IDataResult<List<Post>> GetPosts();
    IDataResult<Post> GetPostByAuthorId(int authorId);
    void AddPost(PostForAddDto postForAddDto, string token);
    IDataResult<Post> UpdatePost(Post post);
    IDataResult<Post> DeletePost(Post post);
    IDataResult<List<PostLikedUsersModel>> GetLikedUsers(List<Post> posts);
}
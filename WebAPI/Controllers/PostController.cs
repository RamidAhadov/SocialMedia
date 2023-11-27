using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Concrete.Dtos;
using DataContracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostService _postService;
        private ICommentService _commentService;
        private IUserService _userService;

        public PostController(IPostService postService, ICommentService commentService, IUserService userService)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
        }
        
        [Authorize]
        [HttpGet]
        [Route("get")]
        public IActionResult GetPosts()
        {
            var posts = _postService.GetPosts();
            if (posts.Success)
            {
                return Ok(posts.Data);
            }
            return BadRequest();
        }

        //[Authorize]
        [HttpGet]
        [Route("getPostsByAuthorId")]
        public IActionResult GetPostsByAuthorId(int authorId)
        {
            var posts = _postService.GetPostsByAuthorId(authorId);
            if (posts.Success)
            {
                return Ok(posts.Data);
            }
            return BadRequest();
        }
        
        [Obsolete]
        [Authorize]
        [HttpGet]
        [Route("getComments")]
        public IActionResult GetComments()
        {
            var comments = _commentService.GetComments();
            if (comments.Success)
            {
                return Ok(comments.Data);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost]
        [Route("getPostLikedUsers")]
        public IActionResult GetPostLikedUsers(List<Post> posts)
        {
            var result = _postService.GetLikedUsers(posts);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest("Not found");
        }
        
        [Authorize]
        [HttpPost]
        [Route("getCommentLikedUsers")]
        public IActionResult GetCommentLikedUsers(List<Comment> comments)
        {
            var result = _commentService.GetLikedUsers(comments);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest("Not found");
        }
        
        [Authorize]
        [HttpPost]
        [Route("addPost")]
        public IActionResult AddPost([FromBody] PostForAddDto postForAddDto)
        {
            var result = _postService.AddPost(postForAddDto,postForAddDto.Token);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("deletePost")]
        public IActionResult DeletePost([FromBody] int postId)
        {
            var result = _postService.DeletePost(postId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("addComment")]
        public IActionResult AddComment([FromBody] CommentForAddModel commentForAddModel)
        {
            var result = _commentService.AddComment(commentForAddModel, commentForAddModel.Token);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("getSearchedUsers")]
        public IActionResult SearchUsers([FromBody] string userName)
        {
            string convertedName = JsonConvert.DeserializeObject<string>(userName);
            var users = _userService.GetBySearch(convertedName);
            return Ok(users);
        }
    }
}

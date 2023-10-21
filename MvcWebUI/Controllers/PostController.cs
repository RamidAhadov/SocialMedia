using System.Text;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete.Dtos;
using DataContracts.Models;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Extensions;
using MvcWebUI.Models;
using MvcWebUI.Services.HeaderService;
using Newtonsoft.Json;

namespace MvcWebUI.Controllers;

public class PostController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetObject<TokenModel>("token");

        var readedUser = TokenReader.DecodeToken(token.Token.Token);
        
        var response = await HeaderService.GetResponseGet(
            "http://localhost:5015", "http://localhost:5015/api/post/get",token);
        
        var model = new HomePageViewModel();
        if (response.IsSuccessStatusCode)
        {
            //List of posts
            string data = response.Content.ReadAsStringAsync().Result;
            var posts = JsonConvert.DeserializeObject<List<Post>>(data);
            model.Posts = posts;
            
            //List of users who liked the current post
            var likePostResponse =  await HeaderService.GetResponsePost(
                "http://localhost:5015", "http://localhost:5015/api/post/getPostLikedUsers", token, posts);
            var postLikedUsers = JsonConvert.DeserializeObject<List<PostLikedUsersModel>>(likePostResponse.Content.ReadAsStringAsync().Result);
            model.PostLikedUsersModels = postLikedUsers;
            
            //List of comments which commented under current post
            var commentResponse = await HeaderService.GetResponseGet(
                "http://localhost:5015", "http://localhost:5015/api/post/getComments", token);
            var comments =
                JsonConvert.DeserializeObject<List<Comment>>(commentResponse.Content.ReadAsStringAsync().Result);
            model.Comments = comments;
            
            //List of users who liked current comment
            var likeCommentResponse =  await HeaderService.GetResponsePost(
                "http://localhost:5015", "http://localhost:5015/api/post/getCommentLikedUsers", token, comments);
            var commentLikedUsers = JsonConvert.DeserializeObject<List<CommentLikedUsersModel>>(likeCommentResponse.Content.ReadAsStringAsync().Result);
            model.CommentLikedUsersModels = commentLikedUsers;

            //List of users who sent friend request to current user
            int userId = readedUser.Id;
            var receivedRequestsResponse = await HeaderService.GetResponsePost(
                "http://localhost:5015", "http://localhost:5015/api/friendRequest/getRequests", token, userId);
            var receivedRequests = JsonConvert.DeserializeObject<List<FriendRequest>>(receivedRequestsResponse.Content.ReadAsStringAsync().Result);
            model.ReceivedRequests = receivedRequests;
            
            //List of friends
            var friendResponse = await HeaderService.GetResponsePost(
                "http://localhost:5015", "http://localhost:5015/api/friend/getFriends", token, userId);
            var friends = JsonConvert.DeserializeObject<List<UserDto>>(friendResponse.Content.ReadAsStringAsync().Result);
            model.Friends = friends;
            
            //Photo
            var photoResponse = await HeaderService.GetResponsePost(
                "http://localhost:5015","http://localhost:5015/api/photo/getProfilePhoto",token,userId);
            var photo = JsonConvert.DeserializeObject<Photo>(photoResponse.Content.ReadAsStringAsync().Result);
            model.PhotoUrl = photo.Url;

            //Current user
            model.UserDto = readedUser;
        }
        return View(model);
    }
    
    public IActionResult AddPost()
    {
        return View();
    }
}
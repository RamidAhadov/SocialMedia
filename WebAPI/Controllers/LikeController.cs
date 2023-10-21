using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using DataContracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        [Route("likePost")]
        public IActionResult LikePost([FromBody] PostLikedUserTokenModel model)
        {
            var result = _likeService.LikePost(model.PostLikedUser, model.Token);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest();
        }
        [HttpPost]
        [Route("likeComment")]
        public IActionResult LikeComment([FromBody] CommentLikedUserTokenModel model)
        {
            var result = _likeService.LikeComment(model.CommentLikedUser, model.Token);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("likeCheck")]
        public IActionResult LikeCheck([FromBody] LikeCheckRequestModel model)
        {
            var result = _likeService.IsLiked(model.CommentId, model.Token);
            if (result.Success)
            {
                return Ok("Liked");
            }

            return Ok("Not liked");
        }
    }
}

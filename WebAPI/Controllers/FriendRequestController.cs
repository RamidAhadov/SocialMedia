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
    public class FriendRequestController : ControllerBase
    {
        private IFriendRequestService _friendRequestService;

        public FriendRequestController(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }

        [HttpPost]
        [Route("getRequests")]
        public IActionResult GetRequests([FromBody] int userId)
        {
            var result = _friendRequestService.ShowUserRequests(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("requestFriend")]
        public IActionResult RequestFriend([FromBody] FriendRequestModel model)
        {
            var result = _friendRequestService.SendRequest(model.Token, model.ReceiverId);
            if (result.Success)
            {
                return Ok("Friend request sent.");
            }

            return Ok("Friend request canceled.");
        }

        [HttpPost]
        [Route("acceptRequest")]
        public IActionResult AcceptRequest([FromBody] ResponseToFriendRequestModel model)
        {
            var result = _friendRequestService.AcceptFriend(model.SenderId,model.ReceiverId);
            if (result.Success)
            {
                return Ok("Request accepted");
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("declineRequest")]
        public IActionResult DeclineRequest([FromBody] ResponseToFriendRequestModel model)
        {
            var result = _friendRequestService.DeclineFriend(model.SenderId,model.ReceiverId);
            if (result.Success)
            {
                return Ok("Request declined");
            }

            return BadRequest();
        }
    }
}

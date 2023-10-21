using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpPost]
        [Route("getFriends")]
        public IActionResult GetFriends([FromBody] int userId)
        {
            var result = _friendService.ShowFriends(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("checkFriend")]
        public IActionResult CheckFriend([FromBody] int userId)
        {
            var result = _friendService.CheckFriend(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
    }
}

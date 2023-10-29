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
        private ISignalRConnectionService _connectionService;

        public FriendController(IFriendService friendService, ISignalRConnectionService connectionService)
        {
            _friendService = friendService;
            _connectionService = connectionService;
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

        [HttpGet]
        [Route("getLastSeen")]
        public IActionResult GetLastSeen(string userName)
        {
            var result = _connectionService.GetLastSeen(userName);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
    }
}

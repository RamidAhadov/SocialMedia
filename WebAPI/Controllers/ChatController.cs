using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataContracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ISignalRConnectionService _connectionService;

        public ChatController(ISignalRConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpPost]
        [Route("recordConnectionId")]
        public IActionResult RecordConnectionId([FromBody] RecordConnectionModel model)
        {
            var result = _connectionService.RecordConnectionId(model.Token, model.ConnectionId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("getConnectionId")]
        public IActionResult GetConnectionId([FromBody] string friendUserName)
        {
            var result = _connectionService.GetConnectionId(friendUserName);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("deleteConnectionId")]
        public IActionResult DeleteConnectionId([FromBody] string connectionId)
        {
            var result = _connectionService.DeleteConnectionId(connectionId);
            if (result.Success)
            {
                return Ok();
            }

            return Ok();
        }
    }
}

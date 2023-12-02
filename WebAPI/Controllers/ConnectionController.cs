using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using DataContracts.Models;
using Entities.Concrete.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private ISignalRConnectionService _connectionService;
        public ConnectionController(ISignalRConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpGet]
        [Route("getConnectionIdById")]
        public IActionResult GetConnectionIdById(int id)
        {
            var result = _connectionService.GetConnectionIdById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
        
        //Returns users active or last connection id
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
        
        //If users another connection id exists in base, method finds and removes it and adds new 
        //connection id to base.
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
        
        
        //Removes users connection id from the base
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
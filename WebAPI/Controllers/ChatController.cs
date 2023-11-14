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
    public class ChatController : ControllerBase
    {
        private ISignalRConnectionService _connectionService;
        private IMessageService _messageService;

        public ChatController(ISignalRConnectionService connectionService, IMessageService messageService)
        {
            _connectionService = connectionService;
            _messageService = messageService;
        }

        //Records new message to base
        [HttpPost]
        [Route("recordMessage")]
        public IActionResult RecordMessage([FromBody] MessageDto messageDto)
        {
            var result = _messageService.RecordMessage(messageDto);
            if (result.Success)
            {
                return Ok();
            }
            return Ok(result.Message);
        }
        
        [Authorize]
        [HttpPost]
        [Route("getChatMessages")]
        public IActionResult GetMessages([FromBody] ChatMessagesModel model)
        {
            var result = _messageService.GetMessages(model.SenderId, model.ReceiverId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return Ok(Messages.NewConversation);
        }

        [HttpPost]
        [Route("updateStatus")]
        public IActionResult UpdateStatus([FromBody] string token)
        {
            var result = _connectionService.UpdateStatus(token);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("checkStatus")]
        public IActionResult CheckStatus([FromBody] string userName)
        {
            var result = _connectionService.CheckStatus(userName);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return Ok(result.Data);
        }
    }
}

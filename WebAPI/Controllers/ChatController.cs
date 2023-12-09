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
        private IUserService _userService;

        public ChatController(ISignalRConnectionService connectionService, IMessageService messageService, IUserService userService)
        {
            _connectionService = connectionService;
            _messageService = messageService;
            _userService = userService;
        }

        //Records new message to base
        [HttpPost]
        [Route("recordMessage")]
        public IActionResult RecordMessage([FromBody] MessageDto messageDto)
        {
            var result = _messageService.RecordMessage(messageDto);
            if (result.Success)
            {
                return Ok(result.Data);
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
        
        [HttpPost]
        [Route("updateMessageStatus")]
        public IActionResult UpdateMessageStatus([FromBody] string messageId)
        {
            var result = _messageService.UpdateMessageStatus(Convert.ToInt32(messageId));
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        [Route("updateMessageStatusOnLogin")]
        public IActionResult UpdateMessageStatusOnLogin([FromBody] string token)
        {
            var messages = _messageService.GetNotReceivedMessages(token);
            if (!messages.Success)
            {
                return BadRequest();
            }

            var updateStatusResult = _messageService.UpdateMessagesStatusOnLogin(messages.Data);
            if (!updateStatusResult.Success)
            {
                return BadRequest();
            }

            var messageSenderUserIds = _userService.GetUserIdsFromMessages(messages.Data);
            if (!messageSenderUserIds.Success)
            {
                return BadRequest();
            }

            var onlineSenderUserIds = _connectionService.GetOnlineUserIds(messageSenderUserIds.Data);
            if (onlineSenderUserIds.Success)
            {
                return Ok(onlineSenderUserIds.Data);
            }

            return BadRequest();
        }
    }
}

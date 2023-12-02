using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.Entities.Concrete.Dtos;
using DataContracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("GetUser")]
        public IActionResult GetUser([FromBody] GetUserRequestModel model)
        {
            var userDto = _userService.GetUserDtoById(model.Id);
            return Ok(userDto);
        }

        [HttpGet]
        [Authorize]
        [Route("getUserByUserName")]
        public IActionResult GetUserByUserName(string userName)
        {
            var result = _userService.GetByUserName(userName);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(Messages.UserNotFound);
        }
        
        [HttpGet]
        [Authorize]
        [Route("getUserByToken")]
        public IActionResult GetUserByToken(string token)
        {
            var result = _userService.GetByToken(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
        
        [HttpGet]
        [Route("getSearchedUsers")]
        public IActionResult SearchUsers(string userName)
        {
            var result = _userService.GetBySearch(userName);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
        
        [HttpGet]
        [Route("getUserById")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetByUserId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
    }
}

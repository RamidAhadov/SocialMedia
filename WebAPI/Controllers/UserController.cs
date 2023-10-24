using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete.Dtos;
using DataContracts.Models;
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

        [HttpPost]
        [Route("getUserName")]
        public IActionResult GetUserName([FromBody] string token)
        {
            var result = _userService.GetByToken(token);
            string userName = result.UserName;
            return Ok(userName);
        }
    }
}

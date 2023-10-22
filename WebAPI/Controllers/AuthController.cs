using Business.Abstract;
using DataContracts.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Claims;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IAspNetAuthService _aspNetAuthService;

        public AuthController(IAuthService authService, IAspNetAuthService aspNetAuthService)
        {
            _authService = authService;
            _aspNetAuthService = aspNetAuthService;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var userResult = _authService.UserControl(model.UserForLoginDto.LoginInfo);
            if (!userResult.Success)
            {
                return BadRequest(userResult.Message);
            }
        
            var result = _authService.Login(model.UserForLoginDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var aspNetResult = await _aspNetAuthService.LoginUser(model.UserForLoginDto);

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
            }
            
            var token = _authService.CreateToken(result.Data);
            if (token.Success) 
            { 
                return Ok(new{Token = token.Data});
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var result = _authService.UserExists(model.UserForRegisterDto.Email,model.UserForRegisterDto.UserName);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            
            var passwordResult = _authService.PasswordWrong(model.UserForRegisterDto.Password, model.ConfirmedPassword);
            if (!passwordResult.Success)
            {
                return BadRequest(passwordResult.Message);
            }

            await _aspNetAuthService.RegisterUser(model.UserForRegisterDto);
            
            var registerResult = _authService.Register(model.UserForRegisterDto);
            var tokenResult = _authService.CreateToken(registerResult.Data);
            if (tokenResult.Success)
            {
                return Ok(new{Token = tokenResult.Data});
            }
        
            return BadRequest("Register failed");
        }
    }
}

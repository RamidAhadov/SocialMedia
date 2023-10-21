using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using DataAccess.Concrete.Dtos;

namespace MvcWebUI.Models;

public class UserViewModel
{
    public UserDto UserDto { get; set; }
    public UserForRegisterDto? UserForRegisterDto { get; set; }
    public UserForLoginDto? UserForLoginDto { get; set; }
    public string? ConfirmedPassword { get; set; }
}
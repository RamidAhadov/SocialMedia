using Core.Entities;

namespace DataAccess.Concrete.Dtos;

public class UserForLoginDto:IDto
{
    public string LoginInfo { get; set; }
    public string Password { get; set; }
}
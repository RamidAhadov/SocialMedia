using DataAccess.Concrete.Dtos;

namespace DataContracts.Models;

public class UserRegisterModel
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string ConfirmedPassword { get; set; }
}
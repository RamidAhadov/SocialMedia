using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Abstract;

public interface IUserService
{
    List<User> GetAll();
    IDataResult<List<UserDto>> GetBySearch(string userName);
    List<OperationClaim> GetClaims(User user);
    void Add(User user);
    User GetById(int id);
    User GetByEmail(string email);
    IDataResult<UserDto> GetByUserName(string userName);
    IDataResult<UserDto> GetByUserId(int id);
    IDataResult<UserDto> GetByToken(string token);
    User GetByLoginInfo(string loginInfo);
    UserDto GetUserDtoById(int id);
    IDataResult<List<int>> GetUserIdsFromMessages(List<Message> messages);
}
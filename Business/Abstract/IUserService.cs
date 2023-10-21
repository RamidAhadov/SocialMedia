using Core.Entities.Concrete;

namespace Business.Abstract;

public interface IUserService
{
    List<User> GetAll();
    List<User> GetBySearch(string userName);
    List<OperationClaim> GetClaims(User user);
    void Add(User user);
    User GetById(int id);
    User GetByEmail(string email);
    User GetByUserName(string userName);
    User GetByLoginInfo(string loginInfo);
}
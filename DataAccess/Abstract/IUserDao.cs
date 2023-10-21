using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserDao:IEntityRepository<User>
{
    List<OperationClaim> GetClaims(User user);
}
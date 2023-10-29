using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;

namespace DataAccess.Abstract;

public interface IUserConnectionIdDao:IEntityRepository<UserConnectionId>
{
   void DeletePreviousRecord(string userName);
   string GetLastConnectionId(string userName);
}
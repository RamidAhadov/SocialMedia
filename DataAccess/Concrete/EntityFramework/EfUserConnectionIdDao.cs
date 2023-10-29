using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserConnectionIdDao:EfEntityRepositoryBase<UserConnectionId,ThinkContext>,IUserConnectionIdDao
{
    public void DeletePreviousRecord(string userName)
    {
        using (var context = new ThinkContext())
        {
            var result = context.UserConnectionIds.FirstOrDefault(uc =>
                uc.UserName == userName);
        }
    }

    public string GetLastConnectionId(string userName)
    {
        using (var context = new ThinkContext())
        {
            var result = context.UserConnectionIds.OrderByDescending(uc => uc.Id)
                .FirstOrDefault(uc => uc.UserName == userName);
            return result.Status;
        }
    }
}
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserConnectionIdDao:EfEntityRepositoryBase<UserConnectionId,ThinkContext>,IUserConnectionIdDao
{
    // public string GetConnectionId(UserDto userDto)
    // {
    //     using (var context = new ThinkContext())
    //     {
    //         var result = from userConnectionId in context.UserConnectionIds
    //             join user in context.Users
    //                 on userConnectionId.UserId equals user.Id
    //             select new
    //             {
    //                 userConnectionId.ConnectionId
    //             };
    //         return result.ToString();
    //     }
    // }
}
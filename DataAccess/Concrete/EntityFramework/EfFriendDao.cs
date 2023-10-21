using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.Dtos;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework;

public class EfFriendDao:EfEntityRepositoryBase<Friend,ThinkContext>,IFriendDao
{
    public List<UserDto> GetFriends(UserDto userDto)
    {
        using (var context = new ThinkContext())
        {
            var result = from user in context.Users
                join friend in context.Friends
                    on user.Id equals friend.UserId
                where friend.FriendId == userDto.Id
                select new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    ProfilePhoto = user.ProfilePhotoUrl
                };
            return result.ToList();
        }
    }
}
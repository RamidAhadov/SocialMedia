using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Dtos;
using DataAccess.Concrete.Dtos;

namespace DataAccess.Abstract;

public interface IFriendDao:IEntityRepository<Friend>
{
    List<UserDto> GetFriends(UserDto userDto);
}
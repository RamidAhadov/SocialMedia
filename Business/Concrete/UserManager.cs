using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserManager:IUserService
{
    private IUserDao _userDao;

    public UserManager(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public List<User> GetAll()
    {
        return _userDao.GetList();
    }

    public List<User> GetBySearch(string userName)
    {
        return _userDao.GetList(u=>u.UserName.Contains(userName));
    }

    public List<OperationClaim> GetClaims(User user)
    {
        return _userDao.GetClaims(user);
    }

    public void Add(User user)
    {
        _userDao.Add(user);
    }

    public User GetById(int id)
    {
        return _userDao.Get(u => u.Id == id);
    }

    public User GetByEmail(string email)
    {
        return _userDao.Get(u => u.Email == email);
    }

    public User GetByUserName(string userName)
    {
        return _userDao.Get(u => u.UserName == userName);
    }

    public User GetByLoginInfo(string loginInfo)
    {
        if (loginInfo.Contains("@"))
        {
            return _userDao.Get(u => u.Email == loginInfo);
        }

        return _userDao.Get(u => u.UserName == loginInfo);
    }
}
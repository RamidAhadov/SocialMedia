using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class PhotoManager:IPhotoService
{
    private IPhotoDao _photoDao;

    public PhotoManager(IPhotoDao photoDao)
    {
        _photoDao = photoDao;
    }

    public IResult UploadPhoto(Photo photo)
    {
        _photoDao.Add(photo);
        return new SuccessResult();
    }

    public IDataResult<Photo> GetProfilePhoto(int userId)
    {
        var photo = _photoDao.Get(p => p.UserId == userId);
        if (photo != null)
        {
            return new SuccessDataResult<Photo>(photo);
        }

        return new ErrorDataResult<Photo>();
    }
}
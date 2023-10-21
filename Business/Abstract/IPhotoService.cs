using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IPhotoService
{
    IResult UploadPhoto(Photo photo);
    IDataResult<Photo> GetProfilePhoto(int userId);
}
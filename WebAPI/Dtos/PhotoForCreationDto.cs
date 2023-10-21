using Core.Entities;

namespace WebAPI.Dtos;

public class PhotoForCreationDto:IDto
{
    public PhotoForCreationDto()
    {
        AddedDate = DateTime.Now;
    }
    public int UserId { get; set; }
    public string Url { get; set; }
    public IFormFile File { get; set; }
    public DateTime AddedDate { get; set; }
    public string PublicId { get; set; }
}
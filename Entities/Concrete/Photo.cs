using Core.Entities;

namespace Entities.Concrete;

public class Photo:IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime AddedDate { get; set; }
    public string Url { get; set; }
    public string PublicId { get; set; }
}
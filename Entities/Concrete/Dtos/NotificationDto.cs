using Core.Entities;

namespace Entities.Concrete.Dtos;

public class NotificationDto:IDto
{
    public string token { get; set; }
    public int ReceiverId { get; set; }
    public string Template { get; set; }
}
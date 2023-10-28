using Core.Entities;

namespace Entities.Concrete.Dtos;

public class MessageDto:IDto
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string MessageText { get; set; }
}
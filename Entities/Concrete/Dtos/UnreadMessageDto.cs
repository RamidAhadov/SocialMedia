using Core.Entities;

namespace Entities.Concrete.Dtos;

public class UnreadMessageDto : IDto
{
    public string ConnectionId { get; set; }
    public List<int> MessageIds { get; set; }
}
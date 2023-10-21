using Microsoft.IdentityModel.Protocols.XmlSignature;

namespace Core.Entities.Concrete;

public class FriendRequest : IEntity
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public int ReceiverId { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
}


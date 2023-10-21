using Core.Entities.Concrete;

namespace DataContracts.Models;

public class FriendRequestModel
{
    public int ReceiverId { get; set; }
    public string Token { get; set; }
}
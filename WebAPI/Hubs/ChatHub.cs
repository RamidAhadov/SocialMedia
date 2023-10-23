using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class ChatHub : Hub
{
    // public Task SendMessage(string user, string message)
    // {
    //     return Clients.All.SendAsync("ReceiveMessage", user, message);
    // }
    public async Task AddUsersToGroup(string connectionId)
    {
        //string groupName = GenerateGroupName(user1, user2);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, "one-to-one");
        await Groups.AddToGroupAsync(connectionId, "one-to-one");
    }
    private string GenerateGroupName(string user1, string user2)
    {
        return $"{user1}-{user2}";
    }
    public async Task SendMessageToGroup(string token, string message)
    {
        var senderUser = TokenReader.DecodeToken(token);
        var senderUserName = senderUser.UserName;
        //string groupName = GenerateGroupName(senderUserName, user2);
    
        await Clients.Group("one-to-one").SendAsync("ReceiveMessage", message);
    }

    // public async Task SendMessageToUser(string connectionId,string message)
    // {
    //     var id = Context.ConnectionId;
    //     await Clients.User(connectionId).SendAsync("ReceiveMessage", message);
    // }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    // public async Task AddUsersToGroup(string user1, string user2)
    // {
    //     string groupName = GenerateGroupName(user1, user2);
    //
    //     await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    // }
    private string GenerateGroupName(string user1, string user2)
    {
        return $"{user1}-{user2}";
    }
    // public async Task SendMessageToGroup(string token, string user2, string message)
    // {
    //     var senderUser = TokenReader.DecodeToken(token);
    //     var senderUserName = senderUser.UserName;
    //     string groupName = GenerateGroupName(senderUserName, user2);
    //
    //     await Clients.Group(groupName).SendAsync("ReceiveMessage", senderUserName, message);
    // }

    // public async Task SendMessageToUser(string connectionId,string message)
    // {
    //     await Clients.User(connectionId).SendAsync("ReceiveMessage", message);
    // }
}
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class ChatHub : Hub
{
    // public Task SendMessage(string user, string message)
    // {
    //     return Clients.All.SendAsync("ReceiveMessage", user, message);
    // }
    public async Task AddUsersToGroup(string token,string friendUserName,string connectionId)
    {
        var userDto = TokenReader.DecodeToken(token);
        string groupName = GenerateGroupName(userDto.UserName, friendUserName);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Groups.AddToGroupAsync(connectionId, groupName);
    }
    
    private string GenerateGroupName(string user1, string user2)
    {
        var sortedStrings = new List<string> { user1, user2 };
        sortedStrings.Sort();

        string combined = string.Join("-", sortedStrings);

        return combined;
    }
    public async Task SendMessageToGroup(string token,string friendName, string message)
    {
        var userDto = TokenReader.DecodeToken(token);
        string groupName = GenerateGroupName(userDto.UserName, friendName);
    
        await Clients.Group(groupName).SendAsync("ReceiveMessage", userDto.UserName,message);
    }

    public async Task RemoveUserFromGroup(string token,string friendName)
    {
        var userDto = TokenReader.DecodeToken(token);
        string groupName = GenerateGroupName(userDto.UserName, friendName);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessageToUser(string connectionId, string message, string user)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveSpecificMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
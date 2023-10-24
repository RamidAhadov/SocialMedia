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
    public async Task SendMessageToGroup(string connectionId,string token,string friendName, string message)
    {
        var userDto = TokenReader.DecodeToken(token);
        string userConnectionId = Context.ConnectionId;
        var messageClass = connectionId == userConnectionId ? "user-message" : "sender-message";
        var formattedMessage = $"<span class='{messageClass}'>{message}</span>";
        string groupName = GenerateGroupName(userDto.UserName, friendName);
    
        await Clients.Group(groupName).SendAsync("ReceiveMessage", userDto.UserName,message);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}
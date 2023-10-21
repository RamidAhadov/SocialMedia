using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    public Task SendMessageToUser(string userId, string message)
    {
        return Clients.User(userId).SendAsync("ReceiveSpecificMessage", message);
    }
}
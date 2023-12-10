using Business.Models;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class ChatHub : Hub
{
    // public Task SendMessage(string user, string message)
    // {
    //     return Clients.All.SendAsync("ReceiveMessage", user, message);
    // }
    public async Task AddUsersToGroup(string userName,string friendUserName,string connectionId)
    {
        string groupName = GenerateGroupName(userName, friendUserName);
        
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
    // public async Task SendMessageToGroup(string token,string friendName, string message, string messageId,string connectionId)
    // {
    //     var userDto = TokenReader.DecodeToken(token);
    //     string groupName = GenerateGroupName(userDto.UserName, friendName);
    //
    //     await Clients.Group(groupName).SendAsync("ReceiveMessage", userDto.UserName,message,messageId,connectionId);
    //     //await Clients.Caller.SendAsync("MessageReceived", $"{friendName}-{messageId}",messageId);
    // }

    public async Task SendMessageToUser(string token, string message, string messageId,string connectionId,string friendConnectionId)
    {
        var userDto = TokenReader.DecodeToken(token);
        await Clients.Client(friendConnectionId).SendAsync("ReceiveMessage", userDto.UserName,message,messageId,connectionId);
    }
    
    public async Task ConfirmMessageReceived(string user,string messageId,string connectionId)
    {
        await Clients.Client(connectionId).SendAsync("MessageReceivedConfirmation", $"{user}-{messageId}",messageId);
    }

    public async Task SendMessageReceiptConfirmation(string connectionId,string messageId)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveReceiptMessageConfirmation", messageId);
    }

    public async Task RemoveUserFromGroup(string token,string friendName)
    {
        var userDto = TokenReader.DecodeToken(token);
        string groupName = GenerateGroupName(userDto.UserName, friendName);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessageToUserObsolette(string connectionId, string message, string user)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveSpecificMessage", user, message);
    }

    // public async Task SendNotification(string connectionId, NotificationModel model)
    // {
    //     await Clients.Client(connectionId).SendAsync("ReceiveNotification", model);
    // }
    
    public async Task SendNotification(string connectionId, string profilePhoto, string notificationContent, string notificationDate, string senderId, string receiverId)
    {
        try
        {
            await Clients.Client(connectionId).SendAsync("ReceiveNotification", profilePhoto, notificationContent,notificationDate,senderId,receiverId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured on hub method: {ex.Message}");
            throw;
        }
    }
    
    public async Task SendFriendRequest(string connectionId, string profilePhoto, string notificationContent, string notificationDate, string senderId, string receiverId)
    {
        try
        {
            await Clients.Client(connectionId).SendAsync("ReceiveFriendRequest", profilePhoto, notificationContent,notificationDate,senderId,receiverId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured on hub method: {ex.Message}");
            throw;
        }
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
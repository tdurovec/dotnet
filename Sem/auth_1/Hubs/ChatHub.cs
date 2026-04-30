using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace auth_1.Hubs;

[Authorize]
public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userName = Context.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(userName))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);
        }
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string receiverName, string message)
    {
        var senderName = Context.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(senderName))
        {
            // Send to the receiver's private group
            await Clients.Group(receiverName).SendAsync("ReceiveMessage", senderName, message);
        }
    }
}

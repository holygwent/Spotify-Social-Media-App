using Microsoft.AspNetCore.SignalR;

namespace SpotifySocialMedia.Hubs
{
    public class CommentHub : Hub
    {
        //public async Task SendMessage(string username,string message)
        //{

        //  await Clients.All.SendAsync("ReceivedMessage",new  { user=username,message=message});
        //}

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
        public async Task SendMessageToGroup(string group, string username, string message)
        {
            await Clients.Group(group).SendAsync("ReceivedMessage", new { user = username, message = message });
        }
        public async Task LeaveGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }
    
    }
}

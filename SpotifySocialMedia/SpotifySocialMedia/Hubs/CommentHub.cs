﻿using Microsoft.AspNetCore.SignalR;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Services.Repositories.Interfaces;

namespace SpotifySocialMedia.Hubs
{
    public class CommentHub : Hub
    {
        private readonly ICommentRepository _commentRepository;


        //public async Task SendMessage(string username,string message)
        //{

        //  await Clients.All.SendAsync("ReceivedMessage",new  { user=username,message=message});
        //}

        public CommentHub(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
        public async Task SendMessageToGroup(string group, string username, string message,string songId,string parent)
        {
            string commentId;
            
           if(parent =="null")
            {
               commentId =  _commentRepository.CreateComment( username,  message,  songId).Result;
                await Clients.Group(group).SendAsync("ReceivedMessage", new
                {
                    commentId = commentId,
                    user = username,
                    message = message,
                    shortDate = DateTime.Now.ToShortDateString(),
                    shortTime = DateTime.Now.ToShortTimeString()
                });
            }
            else
            {
               //create reply 
            }
        

        }
        public async Task LeaveGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }
    
    }
}

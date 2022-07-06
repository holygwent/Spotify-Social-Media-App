using Microsoft.AspNetCore.SignalR;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Services.Repositories.Interfaces;

namespace SpotifySocialMedia.Hubs
{
    public class CommentHub : Hub
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IRateRepository _rateRepository;



        //public async Task SendMessage(string username,string message)
        //{

        //  await Clients.All.SendAsync("ReceivedMessage",new  { user=username,message=message});
        //}

        public CommentHub(ICommentRepository commentRepository, IRateRepository rateRepository)
        {
            _commentRepository = commentRepository;
            _rateRepository = rateRepository;
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendRate(string group, string userEmail, string songId, int value)
        {
            _rateRepository.Add(songId, userEmail, value).Wait();
            var data = _rateRepository.GetAverageRate(songId).Result;
            await Clients.Group(group).SendAsync("ReceiveAverageRate", new
            {
                AverageValue = data.AverageValue,
                NumberOfEvaluators = data.NumberOfEvaluators
            }); 

        }


         public async Task SendReplyToGroup(string group, string username, string message, string songId, string parent)
        {
            string commentId;
            commentId = _commentRepository.CreateReply(username,message, songId, parent).Result;
            await Clients.Group(group).SendAsync("ReceivedReply", new
            {
                commentId = commentId,
                user = username,
                message = message,
                shortDate = DateTime.Now.ToShortDateString(),
                shortTime = DateTime.Now.ToShortTimeString(),
                parent = parent
            });
        }
        
          public async Task SendNotyfication( string group,string username, string message, string songId, string parent)
        {
            var commentAuthorInfo = _commentRepository.GetCommentAuthor(parent).Result;
           
            if (commentAuthorInfo.AuthorEmail !=username )
            {
                await Clients.User(commentAuthorInfo.AuthorId).SendAsync("ReceiveNotify", new
                {
                    communicat = "Someone replied to your comment",
                    songId = songId,
                    group = group
                });
            }
           
        }
        public async Task SendMessageToGroup(string group, string username, string message,string songId,string parent)
        {
            string commentId;
            
           if(parent =="null")
            {
               commentId =  _commentRepository.CreateComment( username,  message,  songId).Result;
                await Clients.Group(group).SendAsync("ReceivedMessage", new
                {
                    songId = songId,
                    commentId = commentId,
                    user = username,
                    message = message,
                    shortDate = DateTime.Now.ToShortDateString(),
                    shortTime = DateTime.Now.ToShortTimeString()
                });
            }
           
        

        }
        public async Task LeaveGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }
    
    }
}

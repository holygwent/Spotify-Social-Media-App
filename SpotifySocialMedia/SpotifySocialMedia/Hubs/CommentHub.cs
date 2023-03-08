using Microsoft.AspNetCore.SignalR;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Services.Repositories.Interfaces;

namespace SpotifySocialMedia.Hubs
{
    public class CommentHub : Hub
    {
        private readonly ICommentService _commentService;
        private readonly IRateService _rateService;
        private readonly INotificationService _notificationService;



        //public async Task SendMessage(string username,string message)
        //{

        //  await Clients.All.SendAsync("ReceivedMessage",new  { user=username,message=message});
        //}

        public CommentHub(ICommentService commentRepository, IRateService rateRepository, INotificationService notificationRepository)
        {
            _commentService = commentRepository;
            _rateService = rateRepository;
            _notificationService = notificationRepository;
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendRate(string group, string userEmail, string songId, int value)
        {
            _rateService.Add(songId, userEmail, value).Wait();
            var data = _rateService.GetAverageRate(songId).Result;
            await Clients.Group(group).SendAsync("ReceiveAverageRate", new
            {
                AverageValue = data.AverageValue,
                NumberOfEvaluators = data.NumberOfEvaluators
            });

        }


        public async Task SendReplyToGroup(string group, string username, string message, string songId, string parent)
        {
            string commentId;
            commentId = _commentService.CreateReply(username, message, songId, parent).Result;
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

        public async Task SendNotyfication(string group, string username, string message, string songId, string parent)
        {
            var commentAuthorInfo = _commentService.GetCommentAuthor(parent).Result;

            if (commentAuthorInfo.AuthorEmail != username)
            {
                _notificationService.AddNotification(commentAuthorInfo.AuthorId, songId).Wait();
                await Clients.User(commentAuthorInfo.AuthorId).SendAsync("ReceiveNotify", new
                {
                    communicat = "Someone replied to your comment",
                    songId = songId,
                    group = group
                });
            }

        }
        public async Task SendMessageToGroup(string group, string username, string message, string songId, string parent)
        {
            string commentId;

            if (parent == "null")
            {
                commentId = _commentService.CreateComment(username, message, songId).Result;
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

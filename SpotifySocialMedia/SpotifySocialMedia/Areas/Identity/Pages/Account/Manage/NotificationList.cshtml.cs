using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SpotifySocialMedia.Areas.Identity.Pages.Account.Manage
{
    public class NotificationListModel : PageModel
    {
        private readonly INotificationService _notificationRepository;

        public NotificationListModel(INotificationService notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Display(Name = "SongId")]
            public string SongId { get; set; }



            [Display(Name = "Song")]
            public string SongName { get; set; }


            [Display(Name = "Artist")]
            public string ArtistName { get; set; }

            [Display(Name = "Added")]
            public DateTime CreatedOn { get; set; }
        }
        public List<NotificationDbo> GetData(string userId)
        {
            var data = _notificationRepository.GetUserNotifications(userId);
            return data;
        }

        public IActionResult OnPostDeleteNotifications(string userId)
        {
            _notificationRepository.DeleteNotifications(userId).Wait();
            return Page();
            
        }
        


    }
}

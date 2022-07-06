using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifySocialMedia.Areas.Identity.Models;
using SpotifySocialMedia.Services;
using System.ComponentModel.DataAnnotations;

namespace SpotifySocialMedia.Areas.Identity.Pages.Account.Manage
{
    public class CommentedSongsListModel : PageModel
    {
        private readonly IUserInformationService _userInformationService;

        public CommentedSongsListModel(IUserInformationService userInformationService)
        {
            _userInformationService = userInformationService;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Display(Name = "SongId")]
            public string SongId { get; set; }



            [Display(Name = "Song")]
            public string Song { get; set; }


            [Display(Name = "Artist")]
            public string Artist { get; set; }

        }
        public List<SongInformation> GetData(string UserId)
        {
            var data = _userInformationService.GetCommentedSongs(UserId);
            return data;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifySocialMedia.Areas.Identity.Models;
using SpotifySocialMedia.Services;
using System.ComponentModel.DataAnnotations;

namespace SpotifySocialMedia.Areas.Identity.Pages.Account.Manage
{
    public class RatingListModel : PageModel
    {
        private readonly IUserInformationService _userInformationService;

        public RatingListModel(IUserInformationService userInformationService)
        {
           _userInformationService = userInformationService;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
          
           
            [Display(Name = "Rate")]
            public int Rate { get; set; }


            [Display(Name = "SongId")]
            public string SongId { get; set; }



            [Display(Name = "Song")]
            public string Song { get; set; }

           
            [Display(Name = "Artist")]
            public string Artist { get; set; }

        }
        public List<UserRating> GetData(string UserId)
        {
           var data =  _userInformationService.GetUserRating(UserId);
            return data;
        }
    }
}

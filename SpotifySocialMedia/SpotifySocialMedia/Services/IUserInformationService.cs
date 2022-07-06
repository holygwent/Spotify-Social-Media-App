using SpotifySocialMedia.Areas.Identity.Models;

namespace SpotifySocialMedia.Services
{
    public interface IUserInformationService
    {
        List<UserRating> GetUserRating(string Id);
        List<SongInformation> GetCommentedSongs(string Id);
    }
}

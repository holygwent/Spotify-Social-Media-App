using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services
{
    public interface ISearchService
    {
       // Task<IEnumerable<SongItem>> SearchSong(string phrase);
        Task<SongItemsTrack> SearchSong(string phrase);
    }
}

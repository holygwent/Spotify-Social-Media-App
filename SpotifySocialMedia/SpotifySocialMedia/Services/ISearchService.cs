using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services
{
    public interface ISearchService
    {
        // Task<IEnumerable<SongItem>> SearchSong(string phrase);
       // Task<Song> GetSong(string Id);
        Task<SongItemsTrack> SearchSong(string phrase);
        Task<SongItemsTrack> GetNextOrPreviousSongs(string url);
    }
}

using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface IArtistService
    {
        Task<SongInfo> AddArtist(string songId);
    }
}

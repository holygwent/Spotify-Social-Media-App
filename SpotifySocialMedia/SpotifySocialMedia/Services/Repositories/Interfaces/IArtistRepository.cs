using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<SongInfo> AddArtist(string songId);
    }
}

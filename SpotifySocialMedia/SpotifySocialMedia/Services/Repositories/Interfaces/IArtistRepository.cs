namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<string> AddArtist(string songId);
    }
}

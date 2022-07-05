using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface IRateRepository
    {
        Task Add(string songId, string userEmail, int rate);
        Task<AverageRate> GetAverageRate();
    }
}

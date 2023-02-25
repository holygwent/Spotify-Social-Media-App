using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface IRateService
    {
        Task Add(string songId, string userEmail, int rate);
        Task<AverageRate> GetAverageRate(string songId);
    }
}

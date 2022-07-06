using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        List<NotificationDbo> GetUserNotifications(string userId);
        Task DeleteNotifications(string userId);
     
        Task AddNotification(string userId, string songId);
    }
}

using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Services.Repositories.Interfaces
{
    public interface INotificationService
    {
        List<NotificationDbo> GetUserNotifications(string userId);
        Task DeleteNotifications(string userId);
     
        Task AddNotification(string userId, string songId);
    }
}

using Dapper;
using Database.Entities;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services.Repositories.Interfaces;
using System.Data;

namespace SpotifySocialMedia.Services.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDbConnection _dbConnection;

        public NotificationRepository(ApplicationDbContext dbContext, IDbConnection dbConnection)
        {
            _dbContext = dbContext;
            _dbConnection = dbConnection;
        }

        public async Task AddNotification(string userId, string songId)
        {
            if (_dbContext.Songs.Any(x => x.Id == songId) & _dbContext.Users.Any(x => x.Id == userId))
            {
                var notification = new Notification() { Id = Guid.NewGuid(), SongId = songId, UserId = userId, Communicat = "Someone replied to your comment", AddedOn = DateTime.Now };
                await _dbContext.Notifications.AddAsync(notification);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteNotification(Guid id)
        {
            var notification = _dbContext.Notifications.SingleOrDefault(x => x.Id == id);
            if (notification is not null)
            {
                _dbContext.Notifications.Remove(notification);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteNotifications(string userId)
        {
            var notifications = _dbContext.Notifications.Where(x => x.UserId == userId);
            if (notifications.Count() > 0)
            {
                _dbContext.Notifications.RemoveRange(notifications);
                await _dbContext.SaveChangesAsync();
            }
        }
        public List<NotificationDbo> GetUserNotifications(string userId)
        {
            const string sql = @"
                           select n.Id as'Id',n.SongId as 'SongId',s.Name as 'SongName',a.Name as 'ArtistName',n.AddedOn as 'CreatedOn'
                            from Notifications n 
                            join Songs s on n.SongId = s.Id
                            join Artists a on a.Id = s.ArtistId
                            where n.UserId = @userId
                                ";
            return _dbConnection.QueryAsync<NotificationDbo>(sql, new { UserId = userId }).Result.ToList();
        }
    }
}

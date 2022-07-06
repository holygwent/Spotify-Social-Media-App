using Dapper;
using SpotifySocialMedia.Areas.Identity.Models;
using System.Data;

namespace SpotifySocialMedia.Services
{
    public class UserInformationService: IUserInformationService
    {
        private readonly IDbConnection _dbConnection;

        public UserInformationService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

        }
        public List<UserRating> GetUserRating(string Id)
        {
            const string sql = @"
                            select s.Id as 'SongId',s.Name as 'SongName',r.Value as 'Rate',a.Name as 'ArtistName'
                            from Songs s
                            join Artists a on s.ArtistId = a.Id
                            join Rates r on r.SongId = s.Id
                            join AspNetUsers aspUser on aspUser.Id = r.UserId
                            where aspUser.Id=@UserId
                                ";

           
            return _dbConnection.QueryAsync<UserRating>(sql, new { UserId = Id}).Result.ToList(); 
        }

        public List<SongInformation> GetCommentedSongs(string Id)
        {
            const string sql = @"
                            select distinct s.Id as 'SongId',s.Name as 'SongName',a.Name as 'ArtistName'
                            from Songs s
                            join Artists a on s.ArtistId = a.Id
                            join Comments c on c.SongId = s.Id
                            join AspNetUsers aspUser on c.AuthorId = aspUser.Id
                            where aspUser.Id=@UserId
                                ";


            return _dbConnection.QueryAsync<SongInformation>(sql, new { UserId = Id }).Result.ToList();
        }


    }
}

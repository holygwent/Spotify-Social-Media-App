using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpotifySocialMedia.SpotifyAuthorizationDataDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySocialMedia.SpotifySettingsDatabase.Services
{
    public interface IDatabaseSpotifyTokenService
    {
        SpotifyToken GetToken();
        void AddToken(SpotifyToken token);
        void DropToken();
    }
    public  class DatabaseSpotifyTokenService: IDatabaseSpotifyTokenService
    {
        private readonly IMongoCollection<SpotifyToken> _token;

        public DatabaseSpotifyTokenService(IOptions<MongoDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _token = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<SpotifyToken>(options.Value.TokenCollectionName);
        }
        public SpotifyToken GetToken() => _token.Find(token => true).SingleOrDefault();
        public void AddToken(SpotifyToken token)
        {
            try
            {
                _token.InsertOne(token);
            }
            catch (Exception)
            {

                throw new Exception("Błąd przesłania tokenu do bazy");
            }
        }
        public void DropToken() => _token.DeleteMany(x => true);
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpotifySocialMedia.SpotifySettingsDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySocialMedia.SpotifySettingsDatabase.Services
{
    public interface IDatabaseAuthorizationCodeService
    {
        string GetCode();
        void AddCode(AuthorizationCode authCode);
        void DropCode();
    }
    public  class DatabaseAuthorizationCodeService : IDatabaseAuthorizationCodeService
    {
        private readonly IMongoCollection<AuthorizationCode> _code;

        public DatabaseAuthorizationCodeService(IOptions<MongoDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _code = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<AuthorizationCode>(options.Value.AuthorizationCodeCollectionName);
        }

        public string GetCode() => _code.Find(x => true).Single().Code;
        public void AddCode(AuthorizationCode authCode) => _code.InsertOne(authCode);
        public void DropCode() => _code.DeleteMany(x => true);

    }
}

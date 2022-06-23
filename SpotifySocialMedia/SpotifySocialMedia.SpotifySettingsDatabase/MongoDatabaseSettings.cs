using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySocialMedia.SpotifySettingsDatabase
{
    public  class MongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AuthorizationCodeCollectionName { get; set; }
        public string TokenCollectionName { get; set; }
    }
}

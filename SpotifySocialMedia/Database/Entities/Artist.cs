using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Genres { get; set; }
        public virtual IEnumerable<Song> Song { get; set; }
    }
}

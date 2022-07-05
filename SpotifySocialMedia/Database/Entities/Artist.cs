using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public  class Artist
    {
        public string Id { get; set; }
        public string genres { get; set; }
        
        public virtual Song Song { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Song
    {
        public string Id { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
        
    }
}

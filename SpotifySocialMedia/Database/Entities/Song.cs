using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Song
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        [ForeignKey("Artist")]
        public string ArtistId { get; set; }
        public virtual IEnumerable<Rate> Rates { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
        public virtual Artist Artist { get; set; }
       
        
    }
}

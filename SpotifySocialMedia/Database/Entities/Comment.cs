using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Comment
    {
       
        public string Id { get; set; }
        [Required]
        public Song Song { get; set; }
        [Required]
        public IdentityUser Author { get; set; }
        public string Content { get; set; }
        public Comment? Parent { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}

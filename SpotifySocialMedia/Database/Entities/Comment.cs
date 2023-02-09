using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        [Required]
        [ForeignKey("Song")]
        public string SongId { get; set; }
        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public string Content { get; set; }
        public string? ParentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Rate { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
        public virtual Song Song { get; set; }
        public virtual IdentityUser Author { get; set; }
        public virtual Comment Parent { get; set; }
    }
}

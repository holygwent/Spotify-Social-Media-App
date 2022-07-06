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
    public  class Notification
    {
        public Guid Id { get; set; }
        public string Communicat { get; set; }
        [Required]
        [ForeignKey("Song")]
        public string SongId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual Song Song { get; set; }
        public virtual IdentityUser User { get; set; }

    }
}

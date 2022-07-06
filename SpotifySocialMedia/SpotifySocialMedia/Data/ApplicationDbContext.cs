using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpotifySocialMedia.Data.OnModelCreatingData;
using SpotifySocialMedia.Models;

namespace SpotifySocialMedia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Roles
            builder.Entity<IdentityRole>(b =>
            {

                b.HasData(OnModelCreateRoles.GetRoles());
            });
            builder.Entity<IdentityUser>(b =>
            {
                b.HasData(OnModelCreatingAdmin.CreateAdmin());
            });
            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasData(OnModelCreatingAddRolesToUsers.AddRolesToUsers());
            });
        }
    }
}
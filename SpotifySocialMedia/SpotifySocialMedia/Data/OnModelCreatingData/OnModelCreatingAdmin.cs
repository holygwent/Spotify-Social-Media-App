using Microsoft.AspNetCore.Identity;

namespace SpotifySocialMedia.Data.OnModelCreatingData
{
    public class OnModelCreatingAdmin
    {
        public static IdentityUser CreateAdmin()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            IdentityUser admin = new IdentityUser()
            {
                Id = Guid.Parse("8931ce67-348b-48b6-96fc-6fc47a74311e").ToString(),
                Email = "Admin@gmail.com",
                UserName = "Admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "999111222",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false,
                PasswordHash = hasher.HashPassword(null, "Admin123!")
  

            };
            return admin;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace SpotifySocialMedia.Data.OnModelCreatingData
{
    public  class OnModelCreateRoles
    {
        public static IdentityRole GetRoles()
        {
            IdentityRole adminRole = new IdentityRole()
            {
                Id = Guid.Parse("7d6f59b9-3f81-4678-9a40-f1d018e711ca").ToString(),
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "ADMIN"

            };
            return adminRole;
        }
    }
}

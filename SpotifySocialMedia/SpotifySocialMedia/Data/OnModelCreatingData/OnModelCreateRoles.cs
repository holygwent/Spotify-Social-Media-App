using Microsoft.AspNetCore.Identity;

namespace SpotifySocialMedia.Data.OnModelCreatingData
{
    public  class OnModelCreateRoles
    {
        public static List<IdentityRole> GetRoles()
        {
            IdentityRole adminRole = new IdentityRole()
            {
                Id = Guid.Parse("7d6f59b9-3f81-4678-9a40-f1d018e711ca").ToString(),
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "ADMIN"

            };
            IdentityRole userRole = new IdentityRole()
            {
                Id = Guid.Parse("284183d9-e14e-46be-a224-aae078fa3456").ToString(),
                Name = "User",
                ConcurrencyStamp = "1",
                NormalizedName = "USER"

            };
            var list = new List<IdentityRole>();
            list.Add(adminRole);
            list.Add(userRole);
            return list;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace SpotifySocialMedia.Data.OnModelCreatingData
{
    public class OnModelCreatingAddRolesToUsers
    {
        public static IdentityUserRole<string> AddRolesToUsers()
        {
            var item = new IdentityUserRole<string>()
            {
                RoleId = Guid.Parse("7d6f59b9-3f81-4678-9a40-f1d018e711ca").ToString(),
                UserId = Guid.Parse("8931ce67-348b-48b6-96fc-6fc47a74311e").ToString()
            };

            return item;
        }
    }
}

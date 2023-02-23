using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
        }

        public virtual ICollection<RoleUser> RoleUsers { get; set; } = new HashSet<RoleUser>();
    }
}
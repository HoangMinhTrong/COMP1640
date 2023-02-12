using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
        }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
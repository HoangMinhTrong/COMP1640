using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser<int>
    {
        public User()
        {

        }

        public DateTime? Birthday { get; set; }
        public UserGenderEnum? Gender { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public virtual ICollection<TenantUser> TenantUsers { get; set; } = new HashSet<TenantUser>();
        public virtual ICollection<UserDepartment> UserDepartments { get; set; } = new HashSet<UserDepartment>();
    }
}
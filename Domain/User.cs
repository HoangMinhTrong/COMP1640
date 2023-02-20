using Microsoft.AspNetCore.Identity;
using Utilities;

namespace Domain
{
    public class User : IdentityUser<int>
    {
        public User()
        {

        }

        public User(string name, string email, DateTime? birthday, UserGenderEnum? gender, Role role, Department department)
        {
            var hasher = new PasswordHasher<User>();

            UserName = name;
            Email = email;
            Birthday = birthday;
            Gender = gender;
            NormalizedUserName = name.ToUpper();
            PasswordHash = hasher.HashPassword(this, DefaultUserProperty.DefaultAccountPassword);

            Roles = new List<Role> { role };
            Departments = new List<Department> { department };
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
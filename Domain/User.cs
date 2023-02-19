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

            RoleUsers = new List<RoleUser> { new RoleUser(this, role) };
            UserDepartments = new List<UserDepartment> { new UserDepartment(this, department) };
        }

        public DateTime? Birthday { get; set; }
        public UserGenderEnum? Gender { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
        public virtual ICollection<RoleUser> RoleUsers { get; set; } = new HashSet<RoleUser>();
        public virtual ICollection<TenantUser> TenantUsers { get; set; } = new HashSet<TenantUser>();
        public virtual ICollection<UserDepartment> UserDepartments { get; set; } = new HashSet<UserDepartment>();

        public void EditInfo(string name
            , string email
            , int roleId
            , int departmentId
            , int? gender
            , string birthday)
        {
            UserName = name;
            Email = email;
            UpdateRole(roleId);
            UpdateDepartment(departmentId);
            UpdateGender(gender);
            //UpdateBirthday(birthday);
        }

        public void UpdateRole(int roleId)
        {
            if (this.RoleUsers.Any(_ => _.RoleId == roleId))
                return;
            RoleUsers = new List<RoleUser> { new RoleUser(this.Id, roleId) };
        }

        public void UpdateDepartment(int departmentId)
        {
            if (this.UserDepartments.Any(_ => _.DepartmentId == departmentId))
                return;
            UserDepartments = new List<UserDepartment> { new UserDepartment(this.Id, departmentId) };
        }

        public void UpdateGender(int? gender)
        {
            Gender = gender.HasValue ? (UserGenderEnum)gender.Value : null;
        }

        public void UpdateBirthday(string birthday)
        {
            Birthday = string.IsNullOrEmpty(birthday) ? DateTime.Parse(birthday) : null;
        }
    }
}
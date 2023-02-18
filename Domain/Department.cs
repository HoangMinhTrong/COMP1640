using Domain.Base;

namespace Domain
{
    public class Department : TenantEntity<int>
    {
        public Department()
        {
        }

        public string Name { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual ICollection<UserDepartment> UserDepartments { get; set; } = new HashSet<UserDepartment>();

    }
}

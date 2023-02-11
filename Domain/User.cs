namespace Domain
{
    public class User
    {
        public User()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public UserGenderEnum? Gender { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public virtual ICollection<Tenant> Tenants { get; set; } = new HashSet<Tenant>();
    }
}

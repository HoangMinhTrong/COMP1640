namespace Domain
{
    public class Role
    {
        public Role()
        {
        }

        public RoleTypeEnum Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}

namespace Domain
{
    public class Department
    {
        public Department()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}

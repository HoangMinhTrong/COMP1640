namespace Domain
{
    public class Tenant
    {
        public Tenant()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}

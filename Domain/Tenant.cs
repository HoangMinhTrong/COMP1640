namespace Domain
{
    public class Tenant
    {
        public Tenant()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TenantUser> TenantUsers { get; set; } = new HashSet<TenantUser>();
    }
}

namespace Domain
{
    public class TenantUser
    {
        public TenantUser()
        {

        }

        public int UserId { get; set; }
        public int TenantId { get; set; }

        public virtual User User { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}

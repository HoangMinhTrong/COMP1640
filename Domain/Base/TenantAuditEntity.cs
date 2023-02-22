namespace Domain.Base
{
    public interface ITenantAuditEntity
    {
        int CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        int TenantId { get; set; }
    }

    public class TenantAuditEntity<T> : BaseEntity<T>, ITenantAuditEntity
    {
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

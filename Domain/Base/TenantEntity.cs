namespace Domain.Base
{
    public interface ITenantEntity
    {
        int TenantId { get; set; }
    }

    public class TenantEntity<T> : BaseEntity<T>, ITenantEntity
    {
        public int TenantId { get; set; }
    }
}

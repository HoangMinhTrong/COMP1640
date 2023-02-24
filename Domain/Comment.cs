using Domain.Base;

namespace Domain
{
    public class Comment : TenantAuditEntity<int>
    {
        public string Content { get; set; }
    }
}

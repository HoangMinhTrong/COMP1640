using Domain.Base;

namespace Domain
{
    public class Comment : TenantAuditEntity<int>
    {
        public int IdeaId { get; set; }
        public string Content { get; set; }

        public virtual Idea Idea { get; set; }
        public virtual User CreatedByNavigation { get; set; }
    }
}

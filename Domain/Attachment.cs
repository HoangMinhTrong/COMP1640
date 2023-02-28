using Domain.Base;

namespace Domain
{
    public class Attachment : TenantAuditEntity<int>
    {
        public Attachment()
        {

        }

        public string Name { get; set; }
        public string KeyName { get; set; }
        public float Size { get; set; }
        public string Extension { get; set; }

        public virtual ICollection<IdeaAttachment> IdeaAttachments { get; set; } = new HashSet<IdeaAttachment>();
    }
}

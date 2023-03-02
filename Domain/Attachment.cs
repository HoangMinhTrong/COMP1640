using Domain.Base;
using Microsoft.AspNetCore.Http;

namespace Domain
{
    public class Attachment : TenantAuditEntity<int>
    {
        public Attachment()
        {

        }

        public Attachment(IFormFile formfile, string fileKey)
        {
            Name = formfile.Name;
            KeyName = fileKey;
            Size = formfile.Length;
            Extension = Path.GetExtension(formfile.Name);
        }

        public string Name { get; set; }
        public string KeyName { get; set; }
        public float Size { get; set; }
        public string Extension { get; set; }

        public virtual ICollection<IdeaAttachment> IdeaAttachments { get; set; } = new HashSet<IdeaAttachment>();
    }
}

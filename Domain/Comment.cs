using Domain.Base;

namespace Domain
{
    public class Comment : TenantAuditEntity<int>
    {
        public Comment()
        {

        }

        public Comment(string content, int ideaId)
        {
            Content = content;
            IdeaId = ideaId;
        }
        public int IdeaId { get; set; }
        public string Content { get; set; }
        public virtual Idea Idea { get; set; }
        public virtual User CreatedByNavigation { get; set; }
    }
}

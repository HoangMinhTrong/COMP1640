using Domain.Base;

namespace Domain
{
    public class Comment : TenantAuditEntity<int>
    {
        public Comment()
        {

        }

        public Comment(string content, int ideaId, bool isAnonymous)
        {
            Content = content;
            IdeaId = ideaId;
            IsAnonymous = isAnonymous;
        }
        public int IdeaId { get; set; }
        public string Content { get; set; }
        public bool IsAnonymous { get; set; }
        public virtual Idea Idea { get; set; }
        public virtual User CreatedByNavigation { get; set; }
    }
}
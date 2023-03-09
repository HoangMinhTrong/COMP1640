using Domain.Base;

namespace Domain
{
    public class IdeaHistory : TenantAuditEntity<int>
    {
        public IdeaHistory()
        {

        }

        public IdeaHistory(Idea idea)
        {
            IdeaId = idea.Id;
            Title = idea.Title;
            Content = idea.Content;
            RealCreatedOn = idea.UpdatedOn;
            CategoryId = idea.CategoryId;
        }

        public int IdeaId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public DateTime RealCreatedOn { get; set; }
        public virtual Idea Idea { get; set; }
        public virtual Category Category { get; set; }
    }
}

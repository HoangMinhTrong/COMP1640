using MediatR;

namespace Domain.DomainEvents
{
    public class IdeaApprovalDomainEvent : INotification
    {
        public IdeaApprovalDomainEvent(Idea idea)
        {
            Idea = idea;
        }

        public Idea Idea { get; set; }
    }
}

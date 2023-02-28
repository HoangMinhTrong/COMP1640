using MediatR;

namespace Domain.DomainEvents
{
    public class CreateIdeaDomainEvent : INotification
    {
        public CreateIdeaDomainEvent(Idea idea)
        {
            Idea = idea;
        }

        public Idea Idea { get; set; }
    }
}

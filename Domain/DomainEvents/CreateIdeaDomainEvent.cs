using MediatR;

namespace Domain.DomainEvents
{
    public class CreateIdeaDomainEvent : INotification
    {
        public Idea Idea { get; set; }
    }
}

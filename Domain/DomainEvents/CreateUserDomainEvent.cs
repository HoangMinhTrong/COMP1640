using MediatR;

namespace Domain.DomainEvents
{
    public class CreateUserDomainEvent : INotification
    {
        public CreateUserDomainEvent(User user)
        {
            this.user = user;
        }

        public User user { get; set; }
    }
}

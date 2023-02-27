using MediatR;

namespace Domain.DomainEvents
{
    public class AddCommentDomainEvent : INotification
    {
        public AddCommentDomainEvent(Comment comment)
        {
            Comment = comment;
        }

        public Comment Comment { get; set; }
    }
}

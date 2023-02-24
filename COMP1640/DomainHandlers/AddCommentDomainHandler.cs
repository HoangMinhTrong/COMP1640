using Domain.DomainEvents;
using MediatR;
using Utilities.EmailService.Interface;
using Utilities.Identity.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class AddCommentDomainHandler : INotificationHandler<AddCommentDomainEvent>
    {
        private readonly ICurrentUserInfo _currentUser;
        private readonly IEmailSender _emailSender;

        public AddCommentDomainHandler(ICurrentUserInfo currentUserInfo
            , IEmailSender emailSender)
        {
            _currentUser = currentUserInfo;
            _emailSender = emailSender;
        }

        public Task Handle(AddCommentDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

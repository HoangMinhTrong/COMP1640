using Domain;
using Domain.DomainEvents;
using MediatR;
using Utilities.EmailService.Interface;
using Utilities.Helpers;
using Utilities.Identity.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class OnCreateIdeaDomainHandler : INotificationHandler<CreateIdeaDomainEvent>
    {
        private readonly ICurrentUserInfo _currentUser;
        private readonly IEmailSender _emailSender;

        public OnCreateIdeaDomainHandler(ICurrentUserInfo currentUserInfo
            , IEmailSender emailSender)
        {
            _currentUser = currentUserInfo;
            _emailSender = emailSender;
        }

        public async Task Handle(CreateIdeaDomainEvent @event, CancellationToken cancellationToken)
        {
            var QACoordinator = @event.Idea?.Department?.QaCoordinator;

            if (QACoordinator == null)
                return;
            var body = RazorViewRenderer.RenderToString("OnIdeaAddedEmailTemplate.cshtml", @event.Idea);
            await _emailSender.SendEmailAsync("Test Mail"
                , body
                , new List<string> { QACoordinator.Email });
        }
    }
}

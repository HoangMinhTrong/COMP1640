using Domain.DomainEvents;
using MediatR;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;
using Utilities.Identity.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class OnCreateIdeaDomainHandler : INotificationHandler<CreateIdeaDomainEvent>
    {
        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly ICurrentUserInfo _currentUser;
        private readonly IEmailSender _emailSender;

        public OnCreateIdeaDomainHandler(ICurrentUserInfo currentUserInfo
            , IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer)
        {
            _currentUser = currentUserInfo;
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
        }

        public async Task Handle(CreateIdeaDomainEvent @event, CancellationToken cancellationToken)
        {
            var QACoordinator = @event.Idea?.Department?.QaCoordinator;
            if (QACoordinator == null)
                return;

            var body = await _razorViewRenderer.RenderToStringAsync("OnIdeaAddedEmailTemplate", @event.Idea);
            if (string.IsNullOrEmpty(body))
                return;

            await _emailSender.SendEmailAsync
                (
                    $"{_currentUser.Name} was created idea: {@event.Idea.Title} "
                    , body
                    , new List<string> { QACoordinator.Email }
                );
        }
    }
}

using Domain.DomainEvents;
using Domain.Interfaces;
using MediatR;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;
using Utilities.Identity.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class AddCommentDomainHandler : INotificationHandler<AddCommentDomainEvent>
    {
        private readonly ICurrentUserInfo _currentUser;
        private readonly IEmailSender _emailSender;
        private readonly IIdeaRepository _ideaRepo;
        private readonly IRazorViewRenderer _razorViewRenderer;

        public AddCommentDomainHandler(ICurrentUserInfo currentUserInfo
            , IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer
            , IIdeaRepository ideaRepo)
        {
            _currentUser = currentUserInfo;
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
            _ideaRepo = ideaRepo;
        }

        public async Task Handle(AddCommentDomainEvent @event, CancellationToken cancellationToken)
        {
            var idea = await _ideaRepo.GetAsync(@event.Comment.IdeaId);
            if (idea == null)
                return;

            var body = await _razorViewRenderer.RenderToStringAsync("OnCommentAddedEmailTemplate", @event.Comment);
            if (string.IsNullOrEmpty(body))
                return;

            await _emailSender.SendEmailAsync
                (
                    $"[COMP1640]: Commented"
                    , body
                    , new List<string> { idea.CreatedByNavigation.Email }
                );
        }
    }
}

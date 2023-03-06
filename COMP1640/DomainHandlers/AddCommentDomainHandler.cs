using COMP1640.ViewModels.EmailModel;
using Domain.DomainEvents;
using Domain.Interfaces;
using MediatR;
using Utilities.Constants;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class AddCommentDomainHandler : INotificationHandler<AddCommentDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IIdeaRepository _ideaRepo;
        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly IConfiguration _configuration;

        public AddCommentDomainHandler(IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer
            , IIdeaRepository ideaRepo
            , IConfiguration configuration)
        {
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
            _ideaRepo = ideaRepo;
            _configuration = configuration;
        }

        public async Task Handle(AddCommentDomainEvent @event, CancellationToken cancellationToken)
        {
            var idea = await _ideaRepo.GetAsync(@event.Comment.IdeaId);
            if (idea == null)
                return;

            var model = new CommentAddedEmailModel(@event.Comment, _configuration.GetSection(AppSetting.APIRoute).Value);
            var body = await _razorViewRenderer.RenderToStringAsync("OnCommentAddedEmailTemplate", model);
            if (string.IsNullOrEmpty(body))
                return;

            await _emailSender.SendEmailAsync
                (
                    $"[COMP1640]: {@event.Comment.CreatedByNavigation.UserName} Commented Your Idea"
                    , body
                    , new List<string> { idea.CreatedByNavigation.Email }
                );
        }
    }
}

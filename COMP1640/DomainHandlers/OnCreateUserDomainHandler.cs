using Domain.DomainEvents;
using MediatR;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class OnCreateUserDomainHandler : INotificationHandler<CreateUserDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewRenderer _razorViewRenderer;

        public OnCreateUserDomainHandler(IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer)
        {
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
        }

        public async Task Handle(CreateUserDomainEvent @event, CancellationToken cancellationToken)
        {
            var body = await _razorViewRenderer.RenderToStringAsync("OnUserAddedEmailTemplate", @event.user);
            if(string.IsNullOrEmpty(body))
                return;

            await _emailSender.SendEmailAsync
                (
                    "[COMP1640] Activate your account"
                    , body
                    , new List<string> { @event.user.Email }
                );
        }
    }
}

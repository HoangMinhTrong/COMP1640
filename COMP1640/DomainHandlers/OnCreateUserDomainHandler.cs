using Domain.DomainEvents;
using MediatR;
using Utilities.EmailService.Interface;
using Utilities.Helpers;

namespace COMP1640.DomainHandlers
{
    public class OnCreateUserDomainHandler : INotificationHandler<CreateUserDomainEvent>
    {
        private readonly IEmailSender _emailSender;

        public OnCreateUserDomainHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(CreateUserDomainEvent @event, CancellationToken cancellationToken)
        {
            var body = RazorViewRenderer.RenderToString("OnIdeaAddedEmailTemplate.cshtml", @event.user);
            // Testing for now
            await _emailSender.SendEmailAsync("Test Mail"
                , "Test body message"
                , new List<string> { @event.user.Email });
        }
    }
}

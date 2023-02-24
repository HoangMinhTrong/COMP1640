using Domain.DomainEvents;
using MediatR;
using Utilities.EmailService.Interface;

namespace COMP1640.DomainHandler
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
            // Testing for now
            var result = _emailSender.SendEmailAsync("Test Mail"
                , "Test body message"
                , new List<string> { @event.user.Email });

            await Task.CompletedTask;
        }
    }
}

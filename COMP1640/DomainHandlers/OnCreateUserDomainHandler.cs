using COMP1640.ViewModels.EmailModel;
using Domain.DomainEvents;
using MediatR;
using Utilities.Constants;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class OnCreateUserDomainHandler : INotificationHandler<CreateUserDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly IConfiguration _configuration;

        public OnCreateUserDomainHandler(IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer
            , IConfiguration configuration)
        {
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
            _configuration = configuration;
        }

        public async Task Handle(CreateUserDomainEvent @event, CancellationToken cancellationToken)
        {
            var model = new UserAddedEmailModel(@event.user, _configuration.GetSection(AppSetting.APIRoute).Value);
            var body = await _razorViewRenderer.RenderToStringAsync("OnUserAddedEmailTemplate", model);
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
